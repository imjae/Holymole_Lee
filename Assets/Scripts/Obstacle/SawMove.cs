using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SawMove : MonoBehaviour
{
    public float mSpeed;
    float railLength;
    float sawLength;
    int direction = 1;

    private void Start()
    {
        mSpeed = 0.01f;
    }
    void Update()
    {
        Moving();
    }

    public void Moving()
    {
        railLength = transform.parent.GetComponent<MeshCollider>().bounds.size.z;//·¹ÀÏÀÇ Æø
        sawLength = gameObject.GetComponent<MeshCollider>().bounds.size.z;//Åé´ÏÀÇ Æø
        
        if (transform.localPosition.z >= Math.Abs(railLength - sawLength))
        {
            direction = -1;
        }
        else if(transform.localPosition.z <= -Math.Abs(railLength - sawLength))
        {
            direction = 1;
        }

        transform.Translate(Vector3.forward * mSpeed * direction);
        Debug.Log(railLength);
        Debug.Log(sawLength);
        Debug.Log(railLength - sawLength);
    }
}
