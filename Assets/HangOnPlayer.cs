using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangOnPlayer : MonoBehaviour
{
    Mole player;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HangOnTarget"))
        {
            player = transform.parent.GetComponent<Mole>();
            player.isHangOn = true;
            player.velocity = Vector3.zero;
            player.animator.SetTrigger("HangOnIdle");

            transform.parent.rotation = other.transform.rotation;
        }            


    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("HangOnTarget"))
            Debug.Log("ÄÝ¸®Àü!");
    }
}
