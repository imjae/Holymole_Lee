using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTriangle : Tangram
{

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            // Debug.Log(other.name);
            if (other.transform.eulerAngles.z > minAngle && other.transform.eulerAngles.z < maxAngle)
            {
                Debug.Log("세번째위치ok");
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
