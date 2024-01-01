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

    public static GameManager Instance { get; private set; }

    public GameState gameState = GameState.InGame;

    public Event disableEvent;

    [SerializeField] private PlayerData turretData;
    [SerializeField] private PlayerData enemyData;


    public enum GameState
    {
        InGame,
        PostGame,
        Paused
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.InGame:
                turretData.DisplayData();
                enemyData.DisplayData();
                break;
            case GameState.PostGame:
                break;
            case GameState.Paused:
                break;
        }
    }

    [System.Serializable]
    struct PlayerData
    {
        public PlayerBase playerBase;
        //data display component refereces
        public BarDisplayer healthDisplayer;
        public BarDisplayer gunHeatDisplayer;
        public TextDisplayer damageDisplayer;

        public void DisplayData()
        {
            if (playerBase == null) return;            
            //gunHeatSlider.transform.position = Camera.main.WorldToScreenPoint((Vector2)playerBase.transform.position + GUNHEAT_SLIDER_OFFSET);
            //healthSlider.transform.position = Camera.main.WorldToScreenPoint((Vector2)playerBase.transform.position + HEALTH_SLIDER_OFFSET);
            healthDisplayer.Display((float)playerBase.health / MAX_PLAYER_HEAlTH);
            gunHeatDisplayer.Display((float)playerBase.gunHeat / MAX_GUN_HEAT);
            //damageText.transform.position = Camera.main.WorldToScreenPoint( );
        }

        
    }
}
