using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    Monster monster;

    void OnTriggerEnter(Collider other)
    {
        monster = transform.parent.GetComponent<Monster>();

        // 대상이 플레이어이고, 죽지 않았을 경우
        if (other.CompareTag("Player") && !other.GetComponent<Player>().IsDie)
        {
            other.SendMessage("KnockBack", (transform.forward + transform.up * 2).normalized * 3f);
            other.SendMessage("TakeDamage", monster.AttackValue);
        }
    }
}
