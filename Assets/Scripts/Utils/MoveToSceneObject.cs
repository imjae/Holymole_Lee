using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSceneObject : Singleton<MoveToSceneObject>
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var count = GameObject.FindObjectsOfType<MoveableToScene>().Length;
        if (transform.childCount < count)
            Destroy(gameObject);
    }
}
