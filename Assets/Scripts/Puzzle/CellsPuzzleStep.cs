using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsPuzzleStep : MonoBehaviour
{
    public GameObject door;
    public GameObject openPoint;
    public GameObject closePoint;
    public static bool isClear;
    public bool isOpen;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")||
           other.CompareTag("Keybox")) 
        {
            isOpen = true;
        }
    }
        void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")||
           other.CompareTag("Keybox")) 
        {
            isOpen = false;
        }
    }
    
    void FixedUpdate()
    {
        if(isClear == true)
        {
            //Clear! 
        }
        else if(isOpen == true)
        {
            if(door.transform.position.y < openPoint.transform.position.y)
            {
                door.transform.Translate(Vector3.up * 0.01f); 
            }
        }
        else if(isOpen == false)
        {
            if(door.transform.position.y > closePoint.transform.position.y)
            {
                door.transform.Translate(Vector3.down * 0.06f);
            }
        }
    }
}
