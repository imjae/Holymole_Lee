using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableToScene : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find(gameObject.name))
            Debug.Log("이미 있음 !");
        else
            Debug.Log(" 없음 !");

    }
}
