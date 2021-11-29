using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (transform.parent.TryGetComponent<Mole>(out Mole mole))
        {
            Debug.Log("�� ���� !");
            if (other.CompareTag("Ground") && mole.isFalling)
            {
                Debug.Log("�� ���� !");
                mole.IsFallingToggle();

                mole.animator.SetTrigger("FallingToRoll");
            }
        }
    }
}
