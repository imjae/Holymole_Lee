using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Player
{
    public Transform groundCheckPoint;
    public Transform hangOnCheckPoint;
    public LayerMask groundLayer;


    float gravity = -13.62f;
    public CharacterController controller;
    Vector3 direction;
    private Transform _standardTransform;
    public Transform StandardTransform
    {
        get { return _standardTransform; }
        set { _standardTransform = value; }
    }

    public Vector3 velocity;
    public Animator animator;

    private bool _isKey;

    public bool IsKey
    {
        get => _isKey;
        set
        {
            _isKey = value;
            var key = GameManager.Instance.mole.transform.Find("Key");
            key.GetChild(0).gameObject.SetActive(_isKey);
        }
    }
    bool isDancing = false;


    void Start()
    {
        if (!TryGetComponent<CharacterController>(out controller))
            Debug.LogError("CharacterController 컴포넌트 없습니다.");
        if (!TryGetComponent<Animator>(out animator))
            Debug.LogError("Animator 컴포넌트 없습니다.");

        Health = gameObject.GetComponent<HealthSystem>();

        MoveSpeed = 3f;
        HangOnMoveSpeed = 1f;
        GroundDistance = 0.1f;
        JumpHeight = 1.2f;
        AttackValue = 30f;

        IsMovement = false;
        IsGrounded = true;
        IsAttacked = false;
        IsHangOn = false;
        IsFalling = false;

        Health.hitPoint = 30f;
        Health.maxHitPoint = 30f;
        Health.regenerate = false;
        Health.isDecrease = false;
        Health.GodMode = false;

        // StandardTransform = CameraManager.Instance.currentNode.Value;
        velocity = Vector3.zero;
        IsAttacked = false;

    }

    // Update is called once per frame
    void Update()
    {
        DistanceFromFloor = GetDistanceFromFloor();
        IsGrounded = gameObject.GroundCheck(groundCheckPoint, groundLayer, GroundDistance);

        if (!isDancing)
        {
            #region Move, Jump, Attack
            if (!IsHangOn)
            {
                animator.SetBool("IsHangOn", false);
                ApplyGravity();
                InputMovement();

                // 대쉬 상태가 아닐때 점프와 공격 가능.
                if (!IsDash)
                {
                    if (Input.GetButtonDown("Jump") && IsGrounded)
                        InputJump();

                    if (Input.GetButtonDown("Attack"))
                        InputAttack();
                }
            }
            else
            {
                animator.SetBool("IsHangOn", true);
                InputHangOnMovement();

                if (Input.GetButtonDown("Jump"))
                    InputHangOnJump();
            }
            #endregion

            if (!IsFalling && DistanceFromFloor > 3 && !IsHangOn)
            {
                IsFallingToggle();
                animator.SetTrigger("FallingIdle");
            }

            if (Health.hitPoint <= 0 && !IsDie)
            {
                Die();
            }

            controller.Move(velocity * Time.deltaTime);
        }
    }

    void InputAttack()
    {
        if (IsGrounded)
            Attack();
        else
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            controller.Move(velocity * Time.deltaTime);
            animator.SetTrigger("JumpAttack");

        }
    }

    public int comboStep;
    public bool comboPossible;

    protected override void Attack()
    {
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
        IsAttackedFalse();
        comboPossible = false;
        comboStep = 0;
    }

    public void IsAttackedTrue()
    {
        IsAttacked = true;
    }
    public void IsAttackedFalse()
    {
        IsAttacked = false;
    }

    public void IsDashTrue()
    {
        IsDash = true;
    }
    public void IsDashFalse()
    {
        IsDash = false;
    }

    public void IsFallingToggle()
    {
        IsFalling = !IsFalling;
    }

    void InputMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // StandardTransform = CameraManager.Instance.currentNode.Value;
        StandardTransform = Camera.main.transform;

        direction = StandardTransform.forward * vertical + StandardTransform.right * horizontal;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        float percent = direction.magnitude;
        animator.SetFloat("RunPercent", percent, 0.1f, Time.deltaTime);

        float finalSpeed = MoveSpeed;

        if (IsAttacked)
            finalSpeed = MoveSpeed / 3f;

        else if (IsFalling)
            finalSpeed = MoveSpeed / 2f;

        controller.Move(Vector3.Scale(direction, new Vector3(1f, 0f, 1f)).normalized * finalSpeed * Time.deltaTime);
    }

    void InputHangOnMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = transform.right * horizontal;

        animator.SetFloat("HangOnMovement", horizontal, 0.1f, Time.deltaTime);

        controller.Move(direction.normalized * HangOnMoveSpeed * Time.deltaTime);
    }

    void InputJump()
    {
        if (IsAttacked)
        {
            animator.SetTrigger("Dash");
            ComboReset();

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // StandardTransform = CameraManager.Instance.currentNode.Value;
            StandardTransform = Camera.main.transform;

            direction = StandardTransform.forward * vertical + StandardTransform.right * horizontal;

            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            float percent = direction.magnitude;
            animator.SetFloat("RunPercent", percent, 0.1f, Time.deltaTime);

            float finalSpeed = 80f;

            controller.Move(Vector3.Scale(direction, new Vector3(1f, 0f, 1f)).normalized * finalSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void InputHangOnJump()
    {
        animator.SetTrigger("HangOnJump");
    }

    public void HangOnClimbTranslate()
    {
        Vector3 distance = new Vector3(-0.10125f, 1.4394f, 0.31301f) - new Vector3(-0.0021783f, 0.38276f, -0.031938f);
        controller.Move(distance * 1.2f);

        IsHangOn = false;
    }

    void ApplyGravity()
    {
        if (IsGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;
    }

    float GetDistanceFromFloor()
    {
        float result = 0f;

        Debug.DrawRay(groundCheckPoint.position, (Vector3.down * 10f), Color.red);

        RaycastHit hit;
        if (Physics.Raycast(groundCheckPoint.position, Vector3.down, out hit, 100f))
        {
            result = hit.distance;
        }

        return result;
    }

    protected override void KnockBack(Vector3 knockBackVelocity)
    {
        velocity = knockBackVelocity;
        StartCoroutine(DelayVectorZero(.5f));
    }

    IEnumerator DelayVectorZero(float time)
    {
        yield return new WaitForSeconds(time);
        velocity = Vector3.zero;
    }


    // ????
    protected override void Die()
    {
        Debug.Log("죽음 !");
        animator.SetTrigger("Die");

        // 실행중이던 애니메이션 트리거 전부 종료
        IsDie = true;
        IsAttacked = false;
        IsFalling = false;
        IsGrounded = false;
    }


    public void IsDancingTrue()
    {
        isDancing = true;
    }
    public void IsDancingFalse()
    {
        isDancing = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // 키 획득 로직
        if (other.CompareTag("Key"))
        {
            IsKey = true;
            animator.SetTrigger("SalsaDancing");

            Destroy(other.gameObject);

            transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);

            // transform.LookAt(Camera.main.transform.position - transform.position);
            // Debug.Log(Camera.main.transform);
        }

        if (other.CompareTag("Door"))
        {
            IsKey = false;
            
            if(other.TryGetComponent<Door>(out Door door))
            {
                // 문이 열렸다는 플래그 변수 셋팅(콜라이더를 disable 시킨다.)
                door.IsOpened = true;
            }

            if (other.TryGetComponent<Animator>(out Animator animator))
            {
                animator.SetTrigger("DoorKeyInsert");
            }

            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).TryGetComponent<DoorRotateInteractable>(out DoorRotateInteractable interactable))
                {
                    interactable.enabled = true;
                }
            }

        }
    }
}
