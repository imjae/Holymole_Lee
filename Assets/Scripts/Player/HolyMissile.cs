using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMissile : MonoBehaviour
{
    public GameObject hitEffect;
    Player player;

    void OnTriggerEnter(Collider other)
    {
        player = transform.parent.GetComponent<Player>();

        if (other.GetComponent<Destroyable>())
            other.gameObject.SendMessage("Damage", 2f, SendMessageOptions.DontRequireReceiver);

        if (other.GetComponent<Monster>() != null)
        {
            other.gameObject.SendMessage("KnockBack", transform.forward.normalized * 4f);
            var tmpEffect = Instantiate(hitEffect, other.bounds.center, Quaternion.identity);
            StartCoroutine(DelayDestroy(tmpEffect, 2f));

            other.gameObject.SendMessage("TakeDamage", player.AttackValue);
        }

    }

    IEnumerator DelayDestroy(GameObject o, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(o);
    }
}
