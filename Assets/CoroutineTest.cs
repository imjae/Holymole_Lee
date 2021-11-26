using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCo());


    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator MoveCo()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            transform.Translate(Vector3.forward * 10);
        }
    }
}
