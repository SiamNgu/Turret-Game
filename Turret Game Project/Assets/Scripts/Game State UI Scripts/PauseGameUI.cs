using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameUI : GameStateUIScript
{
    protected override GameManager.GameStateEnum gameState { get; set; } = GameManager.GameStateEnum.Paused;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void SubscribeEvent()
    {
        GameManager.Instance.EnterPausedEvent += () => transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.EnterInGameEvent += () => transform.GetChild(0).gameObject.SetActive(false);
    }
}
