using System;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] protected GunProfileScriptableObject profile;

    [Header("Shoot reference")]
    [SerializeField] protected Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private PlayerSoundsData soundsData; 


    #region Player in game data
    protected bool right = true;
    private float _gunHeat = 0;
    public float GunHeat 
    { 
        get { return _gunHeat; }
        private set 
        {
            GunHeatChangeEvent.Invoke();
            _gunHeat = value; 
        }
    }
    private float _health = GameManager.MAX_PLAYER_HEAlTH;
    public float Health
    {
        get { return _health; }
        set
        {
            HealthChangeEvent?.Invoke(value - _health);
            _health = value;
            if (_health <= 0)
            {
                Die();
            }
        }
    }
    protected float shootSlowdown = 1;
    #endregion

    protected abstract string other { get; set; }

    public delegate void OnHealthChangeHandler(float difference);
    public event OnHealthChangeHandler HealthChangeEvent;

    public Action GunHeatChangeEvent;

    private void Awake()
    {
        HealthChangeEvent += OnHealthChange;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameStateEnum.InGame) return;
        Orbit();
        shootSlowdown = Mathf.MoveTowards(shootSlowdown, 1, 0.008f);
        GunHeat = Mathf.Abs(GunHeat - (Time.deltaTime * profile.cooldownSpeed * shootSlowdown));
    }

    protected abstract void Orbit();
    protected void Shoot()
    {
        //Change Direction
        right = !right;
        if (GunHeat <= GameManager.MAX_GUN_HEAT-1)
        {
            GameManager.Instance.audioManager.PlaySound(soundsData.shootSound);
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<BulletScript>().profile = profile;
            GunHeat += profile.heatupSpeed;
            shootSlowdown = 0;
        }
    }

    private void OnHealthChange(float difference)
    {
        //if player lost health
        if (difference < 0)
        {
            GameManager.Instance.audioManager.PlaySound(soundsData.hitSound);
        }
    }

    private void Die()
    {
        GameManager.Instance.audioManager.PlaySound(soundsData.dieSound);
        Destroy(gameObject);
        GameManager.Instance.TriggerSwitchState(GameManager.GameStateEnum.PostGame, other);
    }
}
