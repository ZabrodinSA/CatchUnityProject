using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int orthographicSize;
    void Start()
    {
        orthographicSize = Camera.main.pixelHeight / 2;
        Camera.main.orthographicSize = orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
