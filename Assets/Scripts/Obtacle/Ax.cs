using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    public float speed;
    public float angle;
    void Start()
    {
        speed = 2.5f;
        angle = 60f;
    }


    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.realtimeSinceStartup * speed) * angle);
    }
}
