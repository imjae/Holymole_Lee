using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferCameraTrigger : MonoBehaviour
{

    public int step;
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if (dir < 90f)
            {
                // ���� ī�޶�
                CameraManager.Instance.NextCamera(step);
            }
            else
            {
                // ���� ī�޶�
                CameraManager.Instance.PreviousCamera(step);
            }
        }
    }
}
