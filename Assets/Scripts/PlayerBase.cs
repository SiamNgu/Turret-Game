using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] protected GunProfileScriptableObject profile;

    [Header("Shoot reference")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    public float gunHeat { get; private set; } = 0f;
    protected bool right = true;
    public float health { get; private set; }

    protected PlayerControls playerControls;
    

    protected virtual void Awake()
    {        
        playerControls = new PlayerControls();
        health = GameManager.MAX_PLAYER_HEAlTH;
    }


    public void InflictDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Rotate();
        gunHeat -= Time.deltaTime * profile.cooldownSpeed;
        gunHeat = Mathf.Max(gunHeat, 0);
    }

    protected virtual void Rotate()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * profile.mobility * (right ? 1 : -1));

    }

    protected void Shoot()
    {
        right = !right;

        if (gunHeat <= GameManager.MAX_GUN_HEAT - 5)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<BulletScript>().profile = profile;
            gunHeat += profile.heatupSpeed;
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
