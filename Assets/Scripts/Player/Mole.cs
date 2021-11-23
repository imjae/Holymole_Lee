using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Player
{
    public float moveSpeed = 10f;
    public bool isMovement = true;
    public float groundDistance = 2f;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    public bool isGrounded = true;


    float gravity = -9.81f;
    public CharacterController controller;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<CharacterController>(out controller))
            Debug.LogError("CharacterController ������Ʈ�� �����ϴ�.");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = gameObject.GroundCheck(groundCheckPosition, groundLayer, groundDistance);

        // ĳ���� �̵�
        controller.Move(moveDirection.InputDirection() * moveSpeed * Time.deltaTime);
    }
}
