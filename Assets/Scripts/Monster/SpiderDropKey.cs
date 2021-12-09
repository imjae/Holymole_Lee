using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderDropKey : Spider
{
    private Rigidbody rigid;


    private void OnEnable()
    {
        // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
        Agent = this.GetComponent<NavMeshAgent>();
        rigid = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        IsAttacked = false;
        IsDie = false;

        target = FindObjectOfType<Player>().gameObject;

        Animator = this.GetComponent<Animator>();
        Health = this.GetComponent<HealthSystem>();

        Debug.Log(target.name);
        Player = target.transform;

        // 몬스터 세부 사항 설정
        Health.hitPoint = 150f;
        Health.maxHitPoint = 150f;
        Health.regenerate = false;
        Health.isDecrease = false;
        Health.GodMode = false;
        AttackValue = 10f;
        AttackRange = 1f;
        SpeedValue = 1.5f;

        // Target의 위치를 탐색하는 딜레이 시간
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

    // TODO 범위 안에 지정 태그가 들어왔을때 동작 정의할 수 있다.
    private void LateUpdate()
    {
        if (!IsDie)
        {
            // 인자로 정해진 AttackRange(공격범위) 안에 물체가 들어오면 Player태그를 가진 객체를 감지해 공격한다.
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
        // 죽지 않았을 때만 플레이어 감지
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

    protected override void Attack()
    {
        // 공격 실행 후 캐릭터 위치를 보게함.
        transform.LookAt(Player.transform);

        // 공격 부위의 콜라이더 작동은 애니메이션중으로 관리

        base.Attack();
    }

    protected override void Die()
    {
        GameObject brokenKey = transform.Find("BrokenKey").gameObject;

        brokenKey.SetActive(true);
        brokenKey.transform.SetParent(transform.parent);

        base.Die();
    }
}