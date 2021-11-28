using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private float originZPos, spikeLength, timer, speed;
    private bool routine; // true = atk, false = return

    //Z축 기준으로 움직이는 함정입니다.

    public void Attack()
    {
        if(transform.position.z >= originZPos + spikeLength)
        {
            //최대치만큼
            routine = false;
        }
        else if(transform.position.z < originZPos + spikeLength)
        {
            transform.Translate(Vector3.forward * speed);
        }
    }
    public void Back()
    {
        speed = 0.01f;
        if(!(transform.position.z <= originZPos))
        {
            transform.Translate(-(Vector3.forward * speed));
            if(transform.position.z <= originZPos)
            {
                timer = 0;
                routine = true;
            }
        }
    }

    void Start()
    {
        routine = true;
        timer = 0;
        originZPos = transform.position.z;
        spikeLength = GetComponent<MeshCollider>().bounds.size.z;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer <= 2)
        {
            speed = 0.001f;
        }
        else if(timer > 2)
        {
            speed = 0.1f;
        }
        if(routine==true)
        {
            Attack();
        }
        else if(routine==false)
        {
            Back();
        }
    }
}
