using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Tangram
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
            if (other.transform.eulerAngles.z < 35 && other.transform.eulerAngles.z > 30)
            {
                Debug.Log("������°��ġok");
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