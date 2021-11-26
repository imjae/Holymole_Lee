using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private float originYPos, spearLength, speed;
    private bool shoot;
    public GameObject spear;

    void OnTriggerEnter(Collider other) 
    {
        if(other.name == "Burrow")
        {
            Debug.Log("충돌");
            shoot = true;
        }
    }

    void Start()
    {
        speed = 0.1f;
        originYPos = spear.transform.position.y;
        spearLength = spear.GetComponent<MeshCollider>().bounds.size.y;
    }

    void Update()
    {
        if(shoot == true && spear.transform.position.y <= originYPos + spearLength)
        {
             spear.transform.Translate(transform.up * speed);
        }
    }
}
