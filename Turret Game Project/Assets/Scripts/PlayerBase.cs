using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] protected GunProfileScriptableObject profile;

    [Header("Shoot reference")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    public float gunHeat { get; private set; } = 0f;
    protected bool right = true;
    public float health;

    protected virtual void Start()
    {
        health = GameManager.MAX_PLAYER_HEAlTH;
    }

    private void Update()
    {
        Orbit();
        gunHeat -= Time.deltaTime * profile.cooldownSpeed;
        gunHeat = Mathf.Max(gunHeat, 0);
    }

    protected virtual void Orbit()
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
}
