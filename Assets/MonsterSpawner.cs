using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Monster monster;
    
    void Start()
    {
        spawnPoint = transform;

        StartCoroutine(IntervalSpawn());
    }

    void Update()
    {

    }

    IEnumerator IntervalSpawn()
    {
        int num = 1;
        while (num <= 5)
        {
            yield return new WaitForSeconds(3f);
            MonsterGenerator gene = gameObject.GetComponent<MonsterGenerator>();
            gene.Spawn(monster, spawnPoint);
        }
    }
}
