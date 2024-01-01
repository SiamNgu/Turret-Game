public class PlayerTurret : PlayerBase
{
    protected override void Awake()
    {
        base.Awake();
        playerControls.Turret.Shoot.performed += ctx => Shoot();
    }
}