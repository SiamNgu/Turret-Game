using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region constant values
    public const int MAX_PLAYER_HEAlTH = 200;
    public const int MAX_GUN_HEAT = 100;
    public const float BULLET_LIFE_TIME = 4;
    private static readonly Vector2 HEALTH_SLIDER_OFFSET = Vector2.up * .8f;
    private static readonly Vector2 GUNHEAT_SLIDER_OFFSET = Vector2.up * 1f;
    private static readonly Vector2 DAMAGE_TEXT_OFFSET = new Vector2(1f, .5f);
    #endregion
    public InputMaster inputMaster;

    public static GameManager Instance { get; private set; }

    public GameState gameState = GameState.InGame;

    public Event disableEvent;

    [field:SerializeField] 
    public PlayerData defenderData { get; private set; }
    [field: SerializeField]
    public PlayerData invaderData { get; private set; }

    [SerializeField] private GameStateUI[] gameStateUI;

    [System.Serializable]
    public struct GameStateUI
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

    [System.Serializable]
    public struct PlayerData
    {
        public PlayerBase playerScript;
        //data display component refereces
        public Slider healthSlider;
        public Slider gunHeatSlider;
        public TMP_Text damageText;

        public void DisplayData()
        {
            UpdateUIPos(gunHeatSlider.transform, GUNHEAT_SLIDER_OFFSET);
            UpdateUIPos(healthSlider.transform, HEALTH_SLIDER_OFFSET);
            UpdateUIPos(damageText.transform, DAMAGE_TEXT_OFFSET);
            healthSlider.value = (float)playerScript.health / MAX_PLAYER_HEAlTH;
            gunHeatSlider.value = (float)playerScript.gunHeat / MAX_GUN_HEAT;
        }

        private void UpdateUIPos(Transform uiElement, Vector2 offset)
        {
            uiElement.transform.position = Camera.main.WorldToScreenPoint((Vector2)playerScript.transform.position + offset);
        }

    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }
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
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.InGame:
                defenderData.DisplayData();
                invaderData.DisplayData();
                MySetActiveUI();
                break;
            case GameState.PostGame:
                MySetActiveUI();
                inputMaster._1V1.Disable();
                break;
            case GameState.Paused:
                MySetActiveUI();
                break;
        }
    }

    private void MySetActiveUI()
    {
        for (int i = 0; i < gameStateUI.Length; i++)
        {
            if (gameStateUI[i].linkedState == gameState) gameStateUI[i].ui.SetActive(true);
            else gameStateUI[i].ui.SetActive(false);
        }
    }

    public void DealDamage(PlayerBase player, int damage)
    {
        if (player==null) return;
        player.health -= damage;
        if (player.health <= 0)
        {
            Destroy(player);
            gameState = GameState.PostGame;
        }
    }
}
