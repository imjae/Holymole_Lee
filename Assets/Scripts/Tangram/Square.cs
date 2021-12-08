using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Tangram
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            Debug.Log(other.name);
            if (other.transform.eulerAngles.z > 30 && other.transform.eulerAngles.z < 35)
            {
                Debug.Log("여섯번째위치ok");
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
