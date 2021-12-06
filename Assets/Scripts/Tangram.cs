using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangram : MonoBehaviour
{
    bool isCorrectPositon;
    
    void Start()
    {
        
    }

    public void SuccessTangram()
    {
        bool isComplete = false;
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i > 7; i++)
        {
            if(other.name == "Piece"+i)
            {
                Debug.Log(other.name);
            }
        }
    }
}
