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
        // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
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

    // TODO 범위 안에 지정 태그가 들어왔을?? 동작 정의할 수 있다.
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
        // 죽지 않았을 때만 플레이어 감지
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
    // 해당 애니메이션이 끝나고나서 동작할 행위 정의
    private void AnimationCompleteToAction(string animationName, Action action)
    {
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
                              Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4)
        {
            action();
        }
    }


    // 아래부터 재정의 함수
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
        // 공격 실행 후 캐릭터 위치를 보게함.
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