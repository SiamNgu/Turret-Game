using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BulletScript : MonoBehaviour
{
    private Animator animator;
    [HideInInspector] public GunProfileScriptableObject profile;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move()
    {
        if (GameManager.Instance.gameState == GameManager.GameStateEnum.Paused) return;
        transform.Translate(Vector3.up * Time.deltaTime * profile.speed, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetTrigger("Hit");
        if (other.tag == "Bullet")
            other.GetComponent<Animator>().SetTrigger("Hit"); 
        else if (other.tag == "Player")
            other.GetComponent<PlayerBase>().Health -= profile.damage;
    }

    public void OnExitStateMachine()
    {
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
