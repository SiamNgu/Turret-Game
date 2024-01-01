using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GunProfileScriptableObject profile;

    [SerializeField] private LayerMask targetMask;
    private void Start()
    {
        Destroy(gameObject, GameManager.BULLET_LIFE_TIME);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * profile.speed, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);
        other.GetComponent<PlayerBase>()?.InflictDamage(profile.damage);
        Destroy(gameObject);
    }

}
