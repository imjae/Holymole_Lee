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
            Debug.Log("트리거 들어옴");
            player = transform.parent.GetComponent<Mole>();
            player.isHangOn = true;
            player.velocity = Vector3.zero;
            player.animator.SetTrigger("HangOnBlendTree");

            transform.parent.rotation = other.transform.rotation;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("HangOnTarget"))
        {
            Debug.Log("트리거 빠져나감");
            player.animator.SetTrigger("RunBlendTree");
            player = transform.parent.GetComponent<Mole>();
            player.isHangOn = false;
        }
    }
}
