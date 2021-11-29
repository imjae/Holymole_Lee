using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spike : MonoBehaviour
{
    private Vector3 originPos;
    private float timer, speed;
    private bool routine; // true = atk, false = return
    public float tempTimer_atk = 2.1f;
    public float tempTimer_back = 3.3f;

    //Z축 기준으로 움직이는 함정입니다.

    void OnTriggerEnter(Collider other)
    {
        Vector3 a = gameObject.transform.forward;
        Vector3 b = gameObject.transform.up;
        Debug.Log("dmdkr11111"); 
        if(other.CompareTag("Player") && timer > 0 && timer < 5)
        {
            // Debug.Log("dmdkr222");
            // other.gameObject.GetComponent<Mole>().velocity = (a + b).normalized * 5f;
            
            Vector3 pushVelocity = (a + b).normalized * 5f;

            other.gameObject.SendMessage("HitSpike", pushVelocity);

            if(other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigid))
            {
                Debug.Log("dmdkr");
                rigid.AddForce((a + b).normalized * 1005f);
            }
        }
    }
    public void Attack()
    {
        if(timer > tempTimer_atk)
        {
            routine = false;
        }
        else if(timer <= tempTimer_atk)
        {
            transform.Translate(Vector3.forward * speed); 
        }
    }
    public void Back()
    {
        speed = 0.01f;
        if(timer < tempTimer_back)
        {
            transform.Translate(-(Vector3.forward * speed));
        }
    }
    void Start()
    {
        originPos = transform.position;
        routine = true;
        timer = 0;
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
        
        if(timer > 5)
        {
            transform.position = originPos;
            timer = 0;
            routine = true;
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
