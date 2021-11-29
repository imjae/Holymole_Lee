using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingCheckPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (transform.parent.TryGetComponent<Mole>(out Mole mole))
        {
            GameObject target = other.gameObject;
            if (target.TryGetComponent<Obtacle>(out Obtacle t))
            {
                mole.animator.SetTrigger("Pushing");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (transform.parent.TryGetComponent<Mole>(out Mole mole))
        {
            GameObject target = other.gameObject;
            if (target.TryGetComponent<Obtacle>(out Obtacle t))
            {
                mole.animator.Play("Run Blend Tree");
            }
        }
    }
}
