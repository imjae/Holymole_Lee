using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapezium : Tangram
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log(other.name);
            if (other.transform.eulerAngles.z > 75 && other.transform.eulerAngles.z < 80 )
            {
                Debug.Log("�׹�°��ġok");
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
