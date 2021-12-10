using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Obtacle
{
    public float speed;
    private float fallingY;
    private Vector3 originPos;

    void Start()
    {
        originPos = transform.position; 
    }
    void Update()
    {
        fallingY = transform.position.y;
        transform.Translate(Vector3.down * (speed/10));
        if(fallingY < -100f)
        {
            transform.position = originPos;
        }
    }
}
