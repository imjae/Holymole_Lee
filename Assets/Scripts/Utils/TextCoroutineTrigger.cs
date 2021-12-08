using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCoroutineTrigger : MonoBehaviour
{   
    public List<string> txt;
    public float time;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.PrintText(txt, time);
        }
    }
}
