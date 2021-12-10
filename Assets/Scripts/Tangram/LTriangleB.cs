using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTriangleB : Tangram
{

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            // Debug.Log(other.name);
            if (other.transform.eulerAngles.z > minAngle && other.transform.eulerAngles.z < maxAngle)
            {
                Debug.Log("�ι�°��ġok");
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
