using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("¿Ã∞≈ æ»∂‰?");
            other.SendMessage("KnockBack", (transform.forward + transform.up*2).normalized * 3f);
        }
    }
}
