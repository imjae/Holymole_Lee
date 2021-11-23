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

        InputMovement();
    }

    void InputMovement()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection =
                    forward * Input.GetAxisRaw("Vertical") +
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
}
