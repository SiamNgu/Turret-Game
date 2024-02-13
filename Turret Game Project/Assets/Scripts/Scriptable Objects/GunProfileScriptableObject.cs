using UnityEngine;

[CreateAssetMenu(fileName = "GunProfileData", menuName = "Scriptable Object/GunProfileScriptableObject", order = 1)]
public class GunProfileScriptableObject : ScriptableObject
{

    public float mobility = 50;

    //Fire rate
    public float cooldownSpeed = 20f;
    public float heatupSpeed = 20f;

    public int damage = 30;

    public float speed = 50;
}
