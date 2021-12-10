using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject monster;
    MonsterGenerator gene;

    public bool isSpawning = false;

    IEnumerator spawnRoutine;

    void Start()
    {
        spawnRoutine = IntervalSpawn(20f);
        gene = gameObject.GetComponent<MonsterGenerator>();
        
    }

    IEnumerator IntervalSpawn(float time)
    {
        while (isSpawning)
        {
            gene.Spawn(monster, spawnPoint);
            yield return new WaitForSeconds(time);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isSpawning = true;
            StartCoroutine(spawnRoutine);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isSpawning = false;
            StopCoroutine(spawnRoutine);
        }
    }
}
