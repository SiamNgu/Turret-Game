using UnityEngine;

public class InGameUI: GameStateUIScript
{
    protected override GameManager.GameStateEnum gameState { get; set; } = GameManager.GameStateEnum.InGame;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void SubscribeEvent()
    {
        GameManager.Instance.EnterInGameEvent += () => transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.EnterPausedEvent += () => transform.GetChild(0).gameObject.SetActive(false);
        GameManager.Instance.EnterPostGameEvent += (string winner) => transform.GetChild(0).gameObject.SetActive(false);
    }

}
