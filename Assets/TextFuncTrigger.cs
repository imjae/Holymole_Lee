using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFuncTrigger : MonoBehaviour
{
    public string txt;
    public float time;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.PrintText(txt, time);
        }
    }
}
