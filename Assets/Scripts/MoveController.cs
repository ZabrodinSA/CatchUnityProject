using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed;
    private Vector3 vector3;
    void Start()
    {
        //speed = 20;
        vector3 = new Vector3(speed * Time.deltaTime, 0);
    }

    void Update()
    {
        vector3 = new Vector3(speed * Time.deltaTime, 0);
        transform.Translate(vector3);
    }
}
