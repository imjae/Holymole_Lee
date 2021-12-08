using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapezium : Tangram
{
    private void Start()
    {
        isCorrectTransform = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log(other.name);
            if (other.transform.eulerAngles.z < -100 && other.transform.eulerAngles.z > -110 )
            {
                Debug.Log("네번째위치ok");
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
