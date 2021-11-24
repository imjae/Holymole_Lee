using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Player
{
    public float moveSpeed = 10f;
    public float hangOnMoveSpeed = 1f;
    public float groundDistance = 2f;
    public Transform groundCheckPoint;
    public Transform hangOnCheckPoint;
    public LayerMask groundLayer;

    public float jumpHeight = 0.1f;


    public bool isMovement = true;
    public bool isGrounded = true;
    public bool isAttacked = false;
    public bool isHangOn = false;


    float gravity = -19.62f;
    public CharacterController controller;
    Vector3 direction;
    public Vector3 velocity;
    public Animator animator;

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
        isGrounded = gameObject.GroundCheck(groundCheckPoint, groundLayer, groundDistance);

        if (!isHangOn)
        {
            animator.SetBool("IsHangOn", false);
            ApplyGravity();
            InputMovement();

            if (Input.GetButtonDown("Jump") && isGrounded)
                InputJump();
        }
        else
        {
            animator.SetBool("IsHangOn", true);
            InputHangOnMovement();

            
            if (Input.GetButtonDown("Jump"))
                InputHangOnJump();
        }
    }

    void InputMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0, vertical);

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        float percent = direction.magnitude;
        animator.SetFloat("RunPercent", percent, 0.1f, Time.deltaTime);

        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);
    }
    void InputHangOnMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = transform.right * horizontal;

        animator.SetFloat("HangOnMovement", horizontal, 0.1f, Time.deltaTime);

        controller.Move(direction.normalized * hangOnMoveSpeed * Time.deltaTime);
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
            animator.SetTrigger("Jump");
            // 일반상태 : 점프
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            controller.Move(velocity * Time.deltaTime);
        }
    }
    void InputHangOnJump()
    {
        animator.SetTrigger("HangOnJump");
        // if(TryGetComponent<Rigidbody>(out Rigidbody rigid))
        // {
        //     rigid.AddForce(transform.forward * -100);
        // }
        //     // 일반상태 : 점프
        // velocity.y = Mathf.Sqrt(jumpHeight/2f * -2f * gravity);
        // controller.Move(velocity * Time.deltaTime);
    }


    void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
