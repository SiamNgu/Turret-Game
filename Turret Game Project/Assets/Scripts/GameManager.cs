using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region constant values
    public const int MAX_PLAYER_HEAlTH = 200;
    public const int MAX_GUN_HEAT = 100;
    public const float BULLET_LIFE_TIME = 4;
    private static readonly Vector2 HEALTH_SLIDER_OFFSET = Vector2.up;
    private static readonly Vector2 GUNHEAT_SLIDER_OFFSET = Vector2.right;
    private static readonly Vector2 DAMAGE_TEXT_OFFSET = Vector2.one;
    #endregion
    #region References
    [field:SerializeField] public PlayerUIReferences defenderUIReferences { get; private set; }
    [field: SerializeField] public PlayerUIReferences invaderUIReferences { get; private set; }
    #endregion
    #region Struct and Enums
    [System.Serializable] public struct GameStateUI
    {
        public GameObject ui;
        public GameStateEnum linkedState;
    }
    [System.Serializable] public struct PlayerUIReferences
    {
        public PlayerBase playerScript;
        //data display component refereces
        public Slider healthSlider;
        public Slider gunHeatSlider;
        public TMP_Text damageText;

        public void UpdatePos()
        {
            UpdateUIPos(gunHeatSlider.transform, GUNHEAT_SLIDER_OFFSET);
            UpdateUIPos(healthSlider.transform, HEALTH_SLIDER_OFFSET);
            UpdateUIPos(damageText.transform, DAMAGE_TEXT_OFFSET);
        }

        private void UpdateUIPos(Transform uiElement, Vector2 offset)
        {
            uiElement.transform.position = Camera.main.WorldToScreenPoint((Vector2)playerScript.transform.position + offset);
        }

    }
    public enum GameStateEnum
    {
        InGame,
        PostGame,
        Paused
    }
    public enum PlayerType
    {
        Invader,
        Defender
    }
    #endregion
    public InputMaster inputMaster;
    public static GameManager Instance { get; private set; }
    public GameStateEnum gameState { get; private set; }

    public event GameStateEventHandler EnterInGameEvent;
    public event EndGameEventHandler EnterPostGameEvent;
    public event GameStateEventHandler EnterPausedEvent;

    public delegate void GameStateEventHandler();
    public delegate void EndGameEventHandler(string loser);

    private void OnDisable()
    {
        inputMaster.Disable();
    }
    
    private void Awake()
    {
        //Singleton initialization
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        /*Input action intitalization*/
        inputMaster = new InputMaster();

        //Subscribing input events
        inputMaster.Pause.Resume.performed += ctx => TriggerSwitchState(GameStateEnum.InGame);
        inputMaster._1V1.Pause.performed += ctx => TriggerSwitchState(GameStateEnum.Paused);
    }

    private void Start()
    {
        /*Game state initialization*/
        TriggerSwitchState(GameStateEnum.InGame);
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameStateEnum.InGame:
                defenderUIReferences.UpdatePos();
                invaderUIReferences.UpdatePos();
                break;
            case GameStateEnum.PostGame:
                break;
            case GameStateEnum.Paused:
                break;
        }
    }
    private void SwitchActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled) { return; }
        inputMaster.Disable();
        actionMap.Enable();
    }

    public void TriggerSwitchState(GameStateEnum newState, string loser = null)
    {
        gameState = newState;
        switch (newState)
        {
            case GameStateEnum.PostGame:
                EnterPostGameEvent?.Invoke(loser);
                SwitchActionMap(inputMaster.PostGame);
                break;
            case GameStateEnum.Paused:
                EnterPausedEvent?.Invoke();
                SwitchActionMap(inputMaster.Pause);
                break;
            case GameStateEnum.InGame:
                EnterInGameEvent?.Invoke();
                SwitchActionMap(inputMaster._1V1);
                break;
        }
    }
}
