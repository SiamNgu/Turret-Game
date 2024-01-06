using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] protected GunProfileScriptableObject profile;

    [Header("Shoot reference")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    protected abstract GameManager.PlayerUIReferences uiReferences { get; set; }

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
            OnHealthChange(Mathf.Abs(value - _health));
            _health = value;
        }
    }

    private void Update()
    {
        Orbit();
        GunHeat = Mathf.Abs(GunHeat - Time.deltaTime * profile.cooldownSpeed);
    }

    protected abstract void Orbit();

    private void OnHealthChange(float damage)
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

        if (GunHeat <= GameManager.MAX_GUN_HEAT - 5)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<BulletScript>().profile = profile;
            GunHeat += profile.heatupSpeed;
        }
    }
}
