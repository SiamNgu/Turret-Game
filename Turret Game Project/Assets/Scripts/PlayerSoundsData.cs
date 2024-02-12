using UnityEngine;

[CreateAssetMenu(fileName = "New Player Sound Data", menuName = "ScriptableObjects/Player Sound Data")]
public class PlayerSoundsData : ScriptableObject
{
    public AudioClip dieSound;
    public AudioClip shootSound;
    public AudioClip hitSound;
}
