using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioManager))]
public class GameManager : MonoBehaviour
{
    #region constant values
    public const int MAX_PLAYER_HEAlTH = 200;
    public const int MAX_GUN_HEAT = 100;
    public const float DEFENDER_SLOWDOWN_RECOVER_SPEED = 1.5f;
    #endregion
    #region Struct and Enums
    public enum GameStateEnum
    {
        InGame,
        PostGame,
        Paused
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
    
    //Reference to the audio manager
    public AudioManager audioManager { get; private set; }

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

        //Accessing components
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        /*Game state initialization*/
        TriggerSwitchState(GameStateEnum.InGame);
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
