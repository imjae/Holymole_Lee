using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : Character
{
    private Animator _animator;
    private Transform _player;
    private NavMeshAgent _agent;
    private HealthSystem _healthSystem;
    private Camera _faceCamera;

    private float _detectionTime;
    private float _detectionIntervalTime;

    private bool _isAttacked;
    private bool _isDie;

    private IEnumerator _detection;

    protected Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }
    protected Transform Player
    {
        get { return _player; }
        set { _player = value; }
    }
    protected NavMeshAgent Agent
    {
        get { return _agent; }
        set { _agent = value; }
    }
    public HealthSystem Health
    {
        get { return _healthSystem; }
        set { _healthSystem = value; }
    }

    protected float DetectionTime
    {
        get { return _detectionTime; }
        set { _detectionTime = value; }
    }

    protected float DetectionIntervalTime
    {
        get { return _detectionIntervalTime; }
        set { _detectionIntervalTime = value; }
    }

    protected bool IsAttacked
    {
        get { return _isAttacked; }
        set { _isAttacked = value; }
    }
    protected bool IsDie
    {
        get { return _isDie; }
        set { _isDie = value; }
    }
    protected Camera FaceCamera
    {
        get { return _faceCamera; }
        set { _faceCamera = value; }
    }
    protected IEnumerator Detection
    {
        get { return _detection; }
        set { _detection = value; }
    }

    // 추가 정의가 필요 없는 경우 virtual 키워드 붙이지 않음.
    protected void DetectionLocationTarget(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    // 몬스터에 설정된 Range값 범위 안에 충돌일어 났을 경우에 취할 행동 정의
    protected void DetectionInRange(float radius, Action<Collider> action)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Collider detectObject = hitColliders[i++];
            action(detectObject);
        }
    }

    // 달리기 상태일때 동작
    protected virtual void OnRunStatus()
    {
        Agent.enabled = true;
        DetectionLocationTarget(Player);
        Animator.SetTrigger("RunTrigger");
    }
    // 달리기 상태일때 동작
    protected virtual void OnRunStatus(Transform target)
    {
        Agent.enabled = true;
        DetectionLocationTarget(target);
        Animator.SetTrigger("RunTrigger");
    }
    // IDLE 상태일때 동작
    protected virtual void OnIdleStatus()
    {
        Animator.SetTrigger("IdleTrigger");
        Agent.enabled = false;
        Agent.velocity = Vector3.zero;
    }
    // IDLE 상태일때 동작
    protected virtual void OnHitStatus()
    {
        Animator.SetTrigger("HitTrigger");
        Agent.enabled = false;
        Agent.velocity = Vector3.zero;
    }
    // 공격
    protected virtual void Attack()
    {
        Agent.enabled = false;
        Agent.velocity = Vector3.zero;
        Animator.SetTrigger("AttackTrigger");
    }
    // 죽음
    protected virtual void Die()
    {
        // 실행중이던 애니메이션 트리거 전부 종료

        IsDie = true;
        Agent.velocity = Vector3.zero;
        Agent.enabled = false;
        IsAttacked = false;

        StopCoroutine(Detection);
    }

    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }

    // 피격당한 방향
    protected virtual Vector3 GetHitDiretion(Transform target)
    {
        return (transform.position - target.position).normalized;
    }

    // 공격 플래그 변수 TRUE
    public virtual void IsAttackedTrue()
    {
        IsAttacked = true;
    }
    // 공격 플래그 변수 FALSE
    public virtual void IsAttackedFalse()
    {
        IsAttacked = false;
    }

    private IEnumerator SwitchDelayIntoAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

}