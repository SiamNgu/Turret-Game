using UnityEngine;

[CreateAssetMenu(fileName = "New Player Sound Data", menuName = "Scriptable Object/Player Sound Data")]
public class PlayerSoundsData : ScriptableObject
{
    public AudioClip dieSound;
    public AudioClip shootSound;
    public AudioClip hitSound;
}
