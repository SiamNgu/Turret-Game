using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public GunProfileScriptableObject profile;

    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameStateEnum.Paused) return;
        transform.Translate(Vector3.up * Time.deltaTime * profile.speed, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerBase>().Health -= profile.damage;
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
