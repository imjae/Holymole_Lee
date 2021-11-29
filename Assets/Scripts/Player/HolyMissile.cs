using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMissile : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // if (other.GetComponent<Attackable>())
        //     other.gameObject.SendMessage("Damage", 2f, SendMessageOptions.DontRequireReceiver);
    }
}
