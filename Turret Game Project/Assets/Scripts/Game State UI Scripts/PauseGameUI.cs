using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : GameStateUIScript
{
    protected override GameManager.GameStateEnum gameState { get; set; } = GameManager.GameStateEnum.Paused;
    protected override void Awake()
    {
        base.Awake();
        #if UNITY_ANDROID || UNITY_IOS
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.TriggerSwitchState(GameManager.GameStateEnum.Paused));
        #else
            transform.GetChild(1).gameObject.SetActive(false);
        #endif
    }
    protected override void SubscribeEvent()
    {
        GameManager.Instance.EnterPausedEvent += () => transform.GetChild(0).gameObject.SetActive(true);
        GameManager.Instance.EnterInGameEvent += () => transform.GetChild(0).gameObject.SetActive(false);
        #if UNITY_ANDROID || UNITY_IOS
                GameManager.Instance.EnterPausedEvent += () => transform.GetChild(1).gameObject.SetActive(false);
                GameManager.Instance.EnterInGameEvent += () => transform.GetChild(1).gameObject.SetActive(true);
        #endif
    }
}
