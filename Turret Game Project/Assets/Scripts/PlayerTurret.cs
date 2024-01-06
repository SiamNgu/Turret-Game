using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurret : PlayerBase
{
    protected override GameManager.PlayerUIReferences uiReferences { get; set; } 
    void Start()
    {
        uiReferences = GameManager.Instance.defenderUIReferences;
        GameManager.Instance.inputMaster._1V1.Defender.performed += ctx => Shoot();
    }

    protected override void Orbit()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * profile.mobility * (right ? 1 : -1));
    }
}