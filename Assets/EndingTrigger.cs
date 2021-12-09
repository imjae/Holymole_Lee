using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    public GameObject endingPos, printingTextLee, printingTextKing; 
    public List<string> leeTxt, kingTxt;
    private float time;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌??");
        if (other.CompareTag("Player"))
        {
            Debug.Log("충돌");
            var mole = other.GetComponent<Mole>();
            mole.controller.enabled = false;
            other.transform.position = endingPos.transform.position;
            UIManager.Instance.PrintText(leeTxt, time);
            UIManager.Instance.PrintText(kingTxt, time);
        }
    }

    void Start()
    {
        leeTxt = new List<string>();
        kingTxt = new List<string>();
        time = 3 * leeTxt.Count;
    }
}

