using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutsidePuzzleStep : MonoBehaviour
{
    public GameObject bridge, stopPoint;
    public List<GameObject> ActivePosList;
    public float bridgePosZ;
    public int puzzleStack; // 3스택 = 클리어
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")||
           other.CompareTag("Keybox")) 
        {
            puzzleStack++;
        }
    }
        void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")||
           other.CompareTag("Keybox")) 
        {
            puzzleStack--;
        }
    }
    private void ActiveCheck(int Counter)
    {
        for(int i=0; i<ActivePosList.Count; i++)
            // ActivePosList[i].SetActive(false);
        
        if(puzzleStack == 0 && bridgePosZ > stopPoint.transform.position.z)
        {
            bridge.transform.Translate(Vector3.down * 0.01f);
        }
        else if(puzzleStack != 0)
        {
            // ActivePosList[Counter].SetActive(true);
            if(bridgePosZ < ActivePosList[Counter].transform.position.z)
            {
                bridge.transform.Translate(Vector3.up * 0.01f);
            }
        }
        
    }

    void Start()
    {
        GameObject[] ActivePos = GameObject.FindGameObjectsWithTag("ActivePos");
        ActivePosList = new List<GameObject>();
        ActivePosList.AddRange(ActivePos);
    }    
    void Update()
    {
        bridgePosZ = bridge.transform.position.z;
        ActiveCheck(puzzleStack);
    }
}
