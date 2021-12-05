using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustSpin : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(10,8,3);    
    }
}
