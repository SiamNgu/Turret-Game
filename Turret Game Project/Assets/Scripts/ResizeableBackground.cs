using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeableBackground : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        float height = 2f * camera.orthographicSize;
        float width = height * camera.aspect;
        transform.localScale = new Vector2(width, height);
    }

}
