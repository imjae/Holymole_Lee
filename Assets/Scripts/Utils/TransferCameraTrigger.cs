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
                // 다음 카메라
                CameraManager.Instance.NextCamera(step);
            }
            else
            {
                // 이전 카메라
                CameraManager.Instance.PreviousCamera(step);
            }
        }
    }
}
