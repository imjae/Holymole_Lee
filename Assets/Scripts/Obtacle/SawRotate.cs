using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SawRotate : Obtacle
{
    public float rSpeed = 1000f;
 

    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime*rSpeed, 0, 0));
    }


}
