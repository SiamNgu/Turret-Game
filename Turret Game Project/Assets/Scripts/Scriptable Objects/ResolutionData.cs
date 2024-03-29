using UnityEngine;
[CreateAssetMenu(fileName = "New Resolution Data", menuName = "Scriptable Object/Resolution Data")]
public class ResolutionData

    : ScriptableObject
{
    [System.Serializable]
    public struct MyResolution
    {
        public int width;
        public int height;
    }
    public MyResolution[] resolutions;
}
