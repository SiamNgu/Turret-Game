using UnityEngine;

[CreateAssetMenu(fileName = "GunProfileData", menuName = "ScriptableObjects/GunProfileScriptableObject", order = 1)]
public class GunProfileScriptableObject : ScriptableObject
{

    public float mobility = 50;

    //Fire rate
    public float cooldownSpeed = 20f;
    public float heatupSpeed = 20f;

    public float damage = 30;

    public float speed = 50;
}
