using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterGenerator : MonsterFactory<GameObject>
{
    protected override GameObject Create(GameObject _type)
    {
        GameObject monster = null;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
        {
            Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@ ?");
            monster = Instantiate<GameObject>(_type, hit.position, Quaternion.identity);
        }
        
        
        return monster;
    }
}