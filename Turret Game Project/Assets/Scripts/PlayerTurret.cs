public class PlayerTurret : PlayerBase
{
    public override GameManager.PlayerType playerType { get { return GameManager.PlayerType.Defender; } set { } }
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.inputMaster._1V1.Defender.performed += ctx => Shoot();
    }
}