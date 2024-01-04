using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public GunProfileScriptableObject profile;

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
        GameManager.Instance.DealDamage(other.GetComponent<PlayerBase>(), profile.damage);
        Destroy(gameObject);
    }

}
