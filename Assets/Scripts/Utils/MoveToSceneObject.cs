using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSceneObject : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<MoveToSceneObject>().Length != 1)
        {
            Destroy(gameObject);
        }
    }
}
