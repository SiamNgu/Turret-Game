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
    public InputMaster inputMaster;
    public static GameManager Instance { get; private set; }
    public GameState gameState = GameState.InGame;
    public Event disableEvent;
    [field:SerializeField] public PlayerUIReferences defenderUIReferences { get; private set; }
    [field: SerializeField] public PlayerUIReferences invaderUIReferences { get; private set; }
    [SerializeField] private GameStateUI[] gameStateUI;
    #region Struct and Enums
    [System.Serializable] public struct GameStateUI
    {
        public GameObject ui;
        public GameState linkedState;
    }
    public enum GameState
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
    #endregion
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        inputMaster = new InputMaster();
        Resume();
        inputMaster.Pause.Resume.performed += ctx => Resume();
        inputMaster._1V1.Pause.performed += ctx => Pause();
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.InGame:
                defenderUIReferences.UpdatePos();
                invaderUIReferences.UpdatePos();
                break;
            case GameState.PostGame:
                break;
            case GameState.Paused:
                break;
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        gameState = GameState.Paused;
        MySetActiveUI();
        SwitchActionMap(inputMaster.Pause);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameState = GameState.InGame;
        MySetActiveUI();
        SwitchActionMap(inputMaster._1V1);
    }

    private void EndGame()
    {
        gameState = GameState.PostGame;
        MySetActiveUI();
        SwitchActionMap(inputMaster.PostGame);
    }

    private void SwitchActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled) { return; }
        inputMaster.Disable();
        actionMap.Enable();
    }

    private void MySetActiveUI()
    {
        for (int i = 0; i < gameStateUI.Length; i++)
        {
            if (gameStateUI[i].linkedState == gameState) gameStateUI[i].ui.SetActive(true);
            else gameStateUI[i].ui.SetActive(false);
        }
    }

    public void DealDamage(PlayerBase otherPlayer, int damage)
    {
        if (otherPlayer==null) return;
        otherPlayer.Health -= damage;

        if (otherPlayer.Health <= 0)
        {
            Destroy(otherPlayer);
            EndGame();
        }
    }
}
