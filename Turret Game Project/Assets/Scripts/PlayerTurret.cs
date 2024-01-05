public class PlayerTurret : PlayerBase
{
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.inputMaster._1V1.Defender.performed += ctx => Shoot();
    }
}