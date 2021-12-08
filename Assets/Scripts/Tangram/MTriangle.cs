using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTriangle : Tangram
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log(other.name);
            if (other.transform.eulerAngles.z > 120 && other.transform.eulerAngles.z < 125)
            {
                Debug.Log("����°��ġok");
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
