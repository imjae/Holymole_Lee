using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Player
{
    public float moveSpeed = 10f;
    public float groundDistance = 2f;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    
    public float jumpHeight = 0.1f;


    public bool isMovement = true;
    public bool isGrounded = true;
    public bool isAttacked = false;


    float gravity = -19.62f;
    public CharacterController controller;
    Vector3 moveDirection;
    public Vector3 velocity;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<CharacterController>(out controller))
            Debug.LogError("CharacterController 컴포넌트가 없습니다.");
        if (!TryGetComponent<Animator>(out animator))
            Debug.LogError("Animator 컴포넌트가 없습니다.");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = gameObject.GroundCheck(groundCheckPosition, groundLayer, groundDistance);

        ApplyGravity();
        
        InputMovement();

        if (Input.GetButtonDown("Jump") && isGrounded)
            InputJump();
    }

    void InputMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        moveDirection =  forward * Input.GetAxisRaw("Vertical") +
                    right * Input.GetAxisRaw("Horizontal");

        float percent;
        float finalSpeed;

        // float percent = ((run) ? 1f : 0.5f) * moveDirection.magnitude;
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            percent = -1f * moveDirection.magnitude;
            finalSpeed = moveSpeed / 2f;
        }
        else
        {
            percent = 1f * moveDirection.magnitude;
            finalSpeed = moveSpeed;
        }

        animator.SetFloat("RunPercent", percent, 0.1f, Time.deltaTime);

        controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);
    }

    void InputJump()
    {
        if (isAttacked)
        {
            // 공격중 : 대쉬
            Debug.Log("대쉬!");
        }
        else
        {
            // 일반상태 : 점프
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            controller.Move(velocity * Time.deltaTime);
        }
    }


    void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
