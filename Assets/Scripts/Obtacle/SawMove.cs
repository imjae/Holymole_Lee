using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SawMove : MonoBehaviour
{
    float mSpeed = 0.1f;
    float railLength;
    float sawLength;

    void Update()
    {
        Moving();
    }

    public void Moving()
    {
        railLength = gameObject.GetComponentInParent<MeshCollider>().bounds.size.x;//·¹ÀÏÀÇ Æø
        sawLength = gameObject.GetComponent<MeshCollider>().bounds.size.x;//Åé´ÏÀÇ Æø
        
        if (transform.localPosition.z <= Math.Abs(railLength / 2 - sawLength / 2))
        {
            transform.Translate(Vector3.forward * mSpeed,Space.Self);
        }
        if(transform.localPosition.z >= -Math.Abs(railLength/2 - sawLength/2))
        {
            transform.Translate(Vector3.forward * -mSpeed, Space.Self);
        }
    }
}
