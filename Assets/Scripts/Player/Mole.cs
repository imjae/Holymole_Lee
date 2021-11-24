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
            Debug.LogError("CharacterController ������Ʈ�� �����ϴ�.");
        if (!TryGetComponent<Animator>(out animator))
            Debug.LogError("Animator ������Ʈ�� �����ϴ�.");
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // �Է¿� ���� ����
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        float percent = direction.magnitude;

        animator.SetFloat("RunPercent", percent, 0.1f, Time.deltaTime);

        controller.Move(direction.normalized * moveSpeed * Time.deltaTime);
    }

    void InputJump()
    {
        if (isAttacked)
        {
            // ������ : �뽬
            Debug.Log("�뽬!");
        }
        else
        {
            // �Ϲݻ��� : ����
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
