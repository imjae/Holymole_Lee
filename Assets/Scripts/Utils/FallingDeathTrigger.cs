using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDeathTrigger : MonoBehaviour
{
    public Transform respawnSpot;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mole = other.GetComponent<Mole>();
            mole.controller.enabled = false;
            other.transform.position = respawnSpot.transform.position;
            mole.controller.enabled = true;
            UIManager.Instance.FadeON();
            StartCoroutine(IsDying(mole));
        }
    }

    IEnumerator IsDying(Player player)
    {
        player.IsDie = true;
        yield return new  WaitForSeconds(3f);
        player.IsDie = false;
    }
}

