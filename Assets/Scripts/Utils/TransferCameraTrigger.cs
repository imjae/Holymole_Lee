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
            
            
            // dir이 90보다 작으면 다음 카메라상태

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
                // 다음 카메라
                CameraManager.Instance.NextCamera(step);
            }
            else if(enterAngle >= 90f && exitAngle >= 90f)
            {
                // 이전 카메라
                CameraManager.Instance.PreviousCamera(step);
            }
            // Debug.Log($"Camera name: {CameraManager.Instance.currentNode.Value.name}");
        }
    }
}
