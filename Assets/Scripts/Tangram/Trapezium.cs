using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapezium : Tangram
{

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == gameObject.tag)
        {
            /*
            if(other.TryGetComponent<Pieces>(out Pieces pieces))
            {
                pieces.okPosition = transform.position;
            }
            */

            Debug.Log(other.name);
            if (other.transform.eulerAngles.z > minAngle && other.transform.eulerAngles.z < maxAngle )
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
