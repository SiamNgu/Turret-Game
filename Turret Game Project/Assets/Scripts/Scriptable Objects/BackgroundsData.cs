using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Backgrounds Data", menuName = "Scriptable Object/Backgrounds Data")]


public class BackgroundsData : ScriptableObject
{
    [System.Serializable]
    public struct BackgroundData
    {
        public string name; public Material material;
    }

    public BackgroundData[] backgroundsArray;
}
