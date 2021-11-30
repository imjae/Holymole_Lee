using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    public float speed;
    void Start()
    {
        speed = 2.5f;
    }


    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.realtimeSinceStartup * speed) * 60f);
    }
}
