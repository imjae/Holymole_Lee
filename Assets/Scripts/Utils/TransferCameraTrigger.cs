using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferCameraTrigger : MonoBehaviour
{
    public int step;

    float enterAngle;
    float exitAngle;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log($"Camera name: {CameraManager.Instance.currentNode.Value.name}");
            enterAngle = Vector3.Angle(transform.position - other.transform.position, transform.forward);
            
            
            // dir�� 90���� ������ ���� ī�޶����

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            exitAngle = Vector3.Angle(transform.forward, other.transform.position - transform.position);


            Debug.Log($"Enter : {enterAngle} / Exit : {exitAngle}");


            if (enterAngle < 90f && exitAngle < 90f)
            {
                // ���� ī�޶�
                CameraManager.Instance.NextCamera(step);
            }
            else if(enterAngle >= 90f && exitAngle >= 90f)
            {
                // ���� ī�޶�
                CameraManager.Instance.PreviousCamera(step);
            }
            // Debug.Log($"Camera name: {CameraManager.Instance.currentNode.Value.name}");
        }
    }
}
