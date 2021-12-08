using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STriangleB : Tangram
{
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log(other.name);
            if (other.transform.eulerAngles.z > 165 && other.transform.eulerAngles.z < 170)
            {
                Debug.Log("일곱번째위치ok");
                isCorrectTransform = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("위치이동");
        isCorrectTransform = false;
    }
}
