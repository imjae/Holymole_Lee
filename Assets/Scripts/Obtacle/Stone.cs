using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float speed;
    private float fallingY;
    private Vector3 originPos;
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Player"))
        {
            //Á×´Â ÀÌº¥Æ®
        }
    }
    void Start()
    {
        originPos = transform.position; 
    }
    void Update()
    {
        fallingY = transform.position.y;
        Debug.Log(fallingY);
        Debug.Log(originPos);
        transform.Translate(Vector3.down * (speed/10));
        if(fallingY < -100f)
        {
            transform.position = originPos;
        }
    }
}
