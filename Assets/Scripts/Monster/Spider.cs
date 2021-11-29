using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        DetectionLocationTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        // agent.SetDestination(target.position);

    }

    void DetectionLocationTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }
}