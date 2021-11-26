using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SawRotate : MonoBehaviour
{
    float rSpeed = 150f;
    //float railLength;
    //float sawLength;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime*rSpeed, 0, 0));
        //railLength = gameObject.GetComponentInParent<MeshCollider>().bounds.size.x;//·¹ÀÏÀÇ Æø
        //sawLength = gameObject.GetComponent<MeshCollider>().bounds.size.x;//Åé´ÏÀÇ Æø

        //if(transform.localPosition.z >= Math.Abs(railLength/2 - sawLength/2))
        //{
        //    transform.Translate(0,0,)
        //}
        //if(transform.localPosition.z <= -Math.Abs(railLength/2 - sawLength/2))
        //{
        //}
    }


}
