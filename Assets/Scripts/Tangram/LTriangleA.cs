using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTriangleA : Tangram
{
 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log(other.name);
            if(other.transform.eulerAngles.z > minAngle && other.transform.eulerAngles.z < maxAngle)
            {
                Debug.Log("ù��°��ġok");
                isCorrectTransform = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("��ġ�̵�");
        isCorrectTransform = false;
    }
}