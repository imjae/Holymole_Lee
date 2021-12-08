using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STriangleA : Tangram
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
            if (other.transform.eulerAngles.z < 215 && other.transform.eulerAngles.z > 210)
            {
                Debug.Log("�ټ���°��ġok");
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
