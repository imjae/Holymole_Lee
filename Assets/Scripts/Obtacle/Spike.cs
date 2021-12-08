using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spike : Obtacle
{
    private Vector3 originPos;
    private float timer, speed;
    private bool routine; // true = atk, false = return
    public float tempTimer_atk = 2.1f;
    public float tempTimer_back = 3.3f;

    //Zì¶? ê¸°ì???œ¼ë¡? ???ì§ì´?Š” ?•¨? •?ž…?‹ˆ?‹¤.

    // void OnTriggerEnter(Collider other)
    // {
    //     Vector3 forwardForce = gameObject.transform.forward;
    //     Vector3 upsideForce = gameObject.transform.up;
    //     if(other.CompareTag("Player") && timer > 0 && timer < 5)
    //     {
    //         var mole = other.GetComponent<Mole>();
    //         mole.controller.enabled = false;
    //         other.transform.position = respawnPos.transform.position;
    //         mole.controller.enabled = true;
    //         UIManager.Instance.FadeOn();

    //         // Debug.Log("dmdkr222");
    //         // other.gameObject.GetComponent<Mole>().velocity = (a + b).normalized * 5f;
            
    //         Vector3 pushVelocity = (forwardForce + upsideForce).normalized * 5f;

    //         other.gameObject.SendMessage("KnockBack", pushVelocity);

    //         if(other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigid))
    //         {
    //             Debug.Log("dmdkr");
    //             rigid.AddForce((forwardForce + upsideForce).normalized * 1005f);
    //         }
    //     }
    // }
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
