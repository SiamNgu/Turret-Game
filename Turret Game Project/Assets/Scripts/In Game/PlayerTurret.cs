using UnityEngine;

public class PlayerTurret : PlayerBase
{
    protected override string other { get; set; } = "Invader";
    void Start() => GameManager.Instance.inputMaster._1V1.Defender.performed += ctx => Shoot();

    protected override void Orbit() => transform.Rotate(Vector3.forward, Time.deltaTime * profile.mobility * (right ? 1 : -1) * shootSlowdown);
}