using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Monster
{
    public GameObject target;

    private void OnEnable()
    {
        // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
        Agent = this.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        IsAttacked = false;

        Animator = this.GetComponent<Animator>();
        Health = this.GetComponent<HealthSystem>();
        Player = target.transform;

        Health.hitPoint = 150f;
        Health.maxHitPoint = 150f;
        Health.regenerate = false;
        Health.isDecrease = false;
        Health.GodMode = false;

        AttackValue = 10f;
        AttackRange = 1f;
        SpeedValue = 3f;

        DetectionTime = 0.5f;
        DetectionIntervalTime = 4f;

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

    // TODO ���� �ȿ� ���� �±װ� ������?? ���� ������ �� �ִ�.
    private void LateUpdate()
    {
        if (!IsDie)
        {
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

    protected virtual IEnumerator DetectionRoutine()
    {
        // ���� �ʾ��� ���� �÷��̾� ����
        while (!IsDie)
        {
            if (!IsAttacked)
            {
                OnIdleStatus();
                yield return new WaitForSeconds(DetectionTime);
                OnWalkStatus();
                yield return new WaitForSeconds(DetectionIntervalTime);
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

    protected override void Die()
    {
        base.Die();
    }

    protected override void Attack()
    {
        // ���� ���� �� ĳ���� ��ġ�� ������.
        transform.LookAt(Player.transform);

        ActiveAttackPoint();

        base.Attack();
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