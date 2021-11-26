using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ax : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.realtimeSinceStartup * 2.5f) * 60f);
    }
}
