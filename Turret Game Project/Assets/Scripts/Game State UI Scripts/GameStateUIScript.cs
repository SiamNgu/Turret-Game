using UnityEngine;

public abstract class GameStateUIScript : MonoBehaviour
{
    protected abstract GameManager.GameStateEnum gameState { get; set; }

    protected virtual void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        SubscribeEvent();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    protected abstract void SubscribeEvent();

}
