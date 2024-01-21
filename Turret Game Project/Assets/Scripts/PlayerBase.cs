using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] protected GunProfileScriptableObject profile;

    [Header("Shoot reference")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    protected abstract GameManager.PlayerUIReferences uiReferences { get; set; }

    #region Player in game data
    protected bool right = true;
    private float _gunHeat = 0;
    public float GunHeat 
    { 
        get { return _gunHeat; }
        private set 
        {
            OnGunHeatChange();
            _gunHeat = value; 
        }
    }
    private float _health = GameManager.MAX_PLAYER_HEAlTH;
    public float Health
    {
        get { return _health; }
        set
        {
            UpdateHealthUi(value - _health);
            _health = value;
            if (_health <= 0)
            {
                Destroy(gameObject);
                GameManager.Instance.TriggerSwitchState(GameManager.GameStateEnum.PostGame, other);
            }
        }
    }
    protected float shootSlowdown = 1;
    #endregion
    protected abstract string other { get; set; }
    private void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameStateEnum.InGame) return;
        Orbit();
        shootSlowdown = Mathf.MoveTowards(shootSlowdown, 1, 0.008f);
        GunHeat = Mathf.Abs(GunHeat - (Time.deltaTime * profile.cooldownSpeed * shootSlowdown));
    }

    protected abstract void Orbit();

    private void UpdateHealthUi(float damage)
    {
        uiReferences.damageText.GetComponent<Animator>().SetTrigger("isDamage");
        uiReferences.damageText.text = damage.ToString();
        uiReferences.healthSlider.value = (float)Health / GameManager.MAX_PLAYER_HEAlTH;
    }

    private void OnGunHeatChange()
    {
        uiReferences.gunHeatSlider.value = (float)GunHeat / GameManager.MAX_GUN_HEAT;
    }

    protected void Shoot()
    {
        right = !right;
        if (GunHeat <= GameManager.MAX_GUN_HEAT-1)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<BulletScript>().profile = profile;
            GunHeat += profile.heatupSpeed;
            shootSlowdown = 0;
        }
    }
}
