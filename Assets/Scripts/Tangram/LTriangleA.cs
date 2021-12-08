using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTriangleA : Tangram
{
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log(other.name);
            if(other.transform.eulerAngles.z > 253 && other.transform.eulerAngles.z < 258)
            {
                Debug.Log("첫번째위치ok");
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