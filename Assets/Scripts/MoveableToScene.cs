using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableToScene : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find(gameObject.name))
            Debug.Log("�̹� ���� !");
        else
            Debug.Log(" ���� !");

    }
}
