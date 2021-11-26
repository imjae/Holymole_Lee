using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLight : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<DirectionalLight>().Length != 1)
        {
            Destroy(gameObject);
        }
    }
}
