using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferCameraTrigger : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if (dir < 90f)
            {
                // ���� ī�޶�
                Debug.Log("����ī�޶�!!");
                CameraManager.Instance.NextCamera();
            }
            else
            {
                // ���� ī�޶�
                CameraManager.Instance.PreviousCamera();
            }
        }
    }
}
