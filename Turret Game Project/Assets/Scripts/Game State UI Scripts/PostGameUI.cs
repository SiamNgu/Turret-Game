public class PostGameUI : GameStateUIScript
{
    protected override GameManager.GameStateEnum gameState { get; set; } = GameManager.GameStateEnum.PostGame;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void SubscribeEvent()
    {
        GameManager.Instance.EnterPostGameEvent += (string winner) => transform.GetChild(0).gameObject.SetActive(true);
    }
}
