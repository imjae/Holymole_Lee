using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.SendMessage("KnockBack", (transform.forward + transform.up).normalized * 3f);
        }
    }
}
