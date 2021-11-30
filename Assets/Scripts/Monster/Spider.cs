using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Monster
{
    public GameObject target;
    private Rigidbody rigid;

    private void OnEnable()
    {
        // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
        Agent = this.GetComponent<NavMeshAgent>();
        rigid = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        IsAttacked = false;

        Animator = this.GetComponent<Animator>();
        Health = this.GetComponent<HealthSystem>();
        Player = target.transform;

        // ���� ���� ���� ����
        Health.hitPoint = 150f;
        Health.maxHitPoint = 150f;
        Health.regenerate = false;
        Health.isDecrease = false;
        Health.GodMode = false;
        AttackValue = 10f;
        AttackRange = 1f;
        SpeedValue = 1.5f;

        // Target�� ��ġ�� Ž���ϴ� ������ �ð�
        DetectionTime = 0f;
        DetectionIntervalTime = 3f;

        Agent.speed = SpeedValue;

        Detection = DetectionRoutine();
        StartCoroutine(Detection);
    }

    void Update()
    {
        if (Health.hitPoint <= 0 && !IsDie)
        {
            Die();
        }
    }

    // TODO ���� �ȿ� ���� �±װ� �������� ���� ������ �� �ִ�.
    private void LateUpdate()
    {
        if (!IsDie)
        {
            // ���ڷ� ������ AttackRange(���ݹ���) �ȿ� ��ü�� ������ Player�±׸� ���� ��ü�� ������ �����Ѵ�.
            DetectionInRange(AttackRange, (detectObject) =>
             {
                 if (detectObject.CompareTag("Player") && !IsAttacked)
                 {
                     IsAttackedTrue();
                     target = detectObject.gameObject;

                     Attack();
                 }
             });
        }
    }

    protected IEnumerator DetectionRoutine()
    {
        WaitForSeconds detectionTime = new WaitForSeconds(DetectionTime);
        WaitForSeconds detectionIntervalTime = new WaitForSeconds(DetectionIntervalTime);
        // ���� �ʾ��� ���� �÷��̾� ����
        while (!IsDie)
        {
            if (!IsAttacked)
            {
                // OnIdleStatus();
                yield return detectionTime;
                OnWalkStatus();
                yield return detectionIntervalTime;
            }
            yield return null;
        }
    }
    // �ش� �ִϸ��̼��� �������� ������ ���� ����
    private void AnimationCompleteToAction(string animationName, Action action)
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
                              Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4)
        {
            action();
        }
    }


    // �Ʒ����� ������ �Լ�
    protected override void OnWalkStatus()
    {
        base.OnWalkStatus();
    }

    protected override void OnIdleStatus()
    {
        base.OnIdleStatus();
    }

    protected override void OnHitStatus()
    {
        base.OnHitStatus();
    }

    protected override void Attack()
    {
        // ���� ���� �� ĳ���� ��ġ�� ������.
        transform.LookAt(Player.transform);

        // ���� ������ �ݶ��̴� �۵��� �ִϸ��̼������� ����

        base.Attack();
    }
    
    protected override void KnockBack(Vector3 knockBackVelocity)
    {
        Agent.velocity = knockBackVelocity;
    }

    public void ActiveAttackPoint()
    {
        var attackPoint = transform.Find("AttackPoint").GetComponent<SphereCollider>();
        attackPoint.enabled = true;
    }
    public void UnActiveAttackPoint()
    {
        var attackPoint = transform.Find("AttackPoint").GetComponent<SphereCollider>();
        attackPoint.enabled = false;
    }
}