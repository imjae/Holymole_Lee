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
    public bool isAttacked { get; set; }
    public bool isHangOn = false;


    float gravity = -19.62f;
    public CharacterController controller;
    Vector3 direction;
    public Vector3 velocity;
    public Animator animator;

    void Awake()
    {
        if (FindObjectsOfType<Player>().Length != 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        isAttacked = false;

        if (!TryGetComponent<CharacterController>(out controller))
            Debug.LogError("CharacterController ������Ʈ�� �����ϴ�.");
        if (!TryGetComponent<Animator>(out animator))
            Debug.LogError("Animator ������Ʈ�� �����ϴ�.");
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

            if (Input.GetButtonDown("Attack"))
                InputAttack();
        }
        else
        {
            animator.SetBool("IsHangOn", true);
            InputHangOnMovement();

            if (Input.GetButtonDown("Jump"))
                InputHangOnJump();
        }

        controller.Move(velocity * Time.deltaTime);
    }
    void InputAttack()
    {
        Attack();
    }

    public int comboStep;
    public bool comboPossible;

    // Į ���� �޺� ���� �Լ�
    public void Attack()
    {
        Debug.Log("Attack �������� " + comboStep);
        if (comboStep == 0)
        {
            animator.Play("LeftPunching");
            comboStep = 1;
            return;
        }
        else
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }
    public void ComboPossible()
    {
        // Debug.Log("Combo Possible");
        comboPossible = true;
    }
    public void Combo()
    {
        // Debug.Log("Combo!" + comboStep);
        switch (comboStep)
        {
            case 1: animator.Play("LeftPunching"); break;
            case 2: animator.Play("RightPunching"); break;
            case 3: animator.Play("LeftPunching"); break;
            case 4: animator.Play("LeftHook"); break;
            case 5: animator.Play("RightCrossPunch"); break;
            default: ComboReset(); break;
        }
    }
    public void ComboReset()
    {
        Debug.Log("�޺� ����");
        comboPossible = false;
        comboStep = 0;
    }
    public void IsAttackedToggle()
    {
        isAttacked = !isAttacked;
    }

    void InputMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0, vertical);

        ;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        float percent = direction.magnitude;
        animator.SetFloat("RunPercent", percent, 0.1f, Time.deltaTime);

        controller.Move(Vector3.Scale(direction, new Vector3(1f, 0f, 1f)).normalized * moveSpeed * Time.deltaTime);
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
            // ������ : �뽬
            Debug.Log("�뽬!");
        }
        else
        {
            animator.SetTrigger("Jump");
            // �Ϲݻ��� : ����
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
        //     // �Ϲݻ��� : ����
        // velocity.y = Mathf.Sqrt(jumpHeight/2f * -2f * gravity);
        // controller.Move(velocity * Time.deltaTime);
    }

    public void HangOnClimbTranslate()
    {
        Vector3 distance = new Vector3(-0.10125f, 1.4394f, 0.31301f) - new Vector3(-0.0021783f, 0.38276f, -0.031938f);
        controller.Move(distance * 1.2f);

        isHangOn = false;
    }

    void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;
    }
}
