using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject monster;
    MonsterGenerator gene;

    void Start()
    {
        spawnPoint = transform;
        gene = gameObject.GetComponent<MonsterGenerator>();
        StartCoroutine(IntervalSpawn());
    }
    IEnumerator IntervalSpawn()
    {
        int num = 1;
        while (num <= 5)
        {
            yield return new WaitForSeconds(3f);
            gene.Spawn(monster, spawnPoint);
        }
    }
}
