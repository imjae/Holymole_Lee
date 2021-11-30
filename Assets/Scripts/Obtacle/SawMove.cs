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
        transform.Translate(Vector3.forward * mSpeed * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FrontCheck")
        {
            direction = -1;
        }
        else if (other.name == "BackCheck")
        {
            direction = 1;
        }
    }
    //public void Moving()
    //{
    //    railLength = transform.parent.GetComponent<MeshCollider>().bounds.size.z;//·¹ÀÏÀÇ Æø
    //    sawLength = gameObject.GetComponent<MeshCollider>().bounds.size.z;//Åé´ÏÀÇ Æø

    //    if (transform.localPosition.z >= Math.Abs(railLength - sawLength))
    //    {
    //        direction = -1;
    //    }
    //    else if(transform.localPosition.z <= -Math.Abs(railLength - sawLength))
    //    {
    //        direction = 1;
    //    }
    //    transform.Translate(Vector3.forward * mSpeed * direction);
    //}

}
