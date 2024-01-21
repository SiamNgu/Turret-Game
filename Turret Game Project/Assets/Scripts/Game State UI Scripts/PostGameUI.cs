using UnityEngine;
using TMPro;

public class PostGameUI : GameStateUIScript
{
    [SerializeField] private TMP_Text title;
    protected override GameManager.GameStateEnum gameState { get; set; } = GameManager.GameStateEnum.PostGame;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void SubscribeEvent()
    {
        GameManager.Instance.EnterPostGameEvent += OnEndGame;
    }

    private void OnEndGame(string winner)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        title.text = winner + " has won the game!";
    }
}
