using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public BackgroundsData backgroundData;

     [SerializeField]private GameObject backgroundObject;
    [HideInInspector] public int selectedBackground;

    public void SetBackground()
    {
        backgroundObject.GetComponent<Renderer>().material = backgroundData.backgroundsArray[selectedBackground].material;
    }
}
