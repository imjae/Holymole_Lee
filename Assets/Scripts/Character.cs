using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float _moveSpeed;
    private float _hangOnMoveSpeed;
    private float _groundDistance;
    private float _jumpHeight;
    private float _distanceFromFloor;

    private float _attackValue;
    private float _attackRange;
    private float _speedValue;
    private HealthSystem _healthSystem;


    private bool _isDie;
    private bool _isMovement;
    private bool _isGrounded;
    private bool _isAttacked;
    private bool _isHangOn;
    private bool _isFalling;


    public float AttackValue { get { return _attackValue; } set { _attackValue = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float SpeedValue { get { return _speedValue; } set { _speedValue = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float HangOnMoveSpeed { get { return _hangOnMoveSpeed; } set { _hangOnMoveSpeed = value; } }
    public float GroundDistance { get { return _groundDistance; } set { _groundDistance = value; } }
    public float JumpHeight { get { return _jumpHeight; } set { _jumpHeight = value; } }
    public float DistanceFromFloor { get { return _distanceFromFloor; } set { _distanceFromFloor = value; } }

    public HealthSystem Health { get { return _healthSystem; } set { _healthSystem = value; } }
    public bool IsDie { get { return _isDie; } set { _isDie = value; } }
    public bool IsMovement { get { return _isMovement; } set { _isMovement = value; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool IsAttacked { get { return _isAttacked; } set { _isAttacked = value; } }
    public bool IsHangOn { get { return _isHangOn; } set { _isHangOn = value; } }
    public bool IsFalling { get { return _isFalling; } set { _isFalling = value; } }

    // °ø°Ý
    protected virtual void Attack()
    { 

    }
    // Á×À½
    protected virtual void Die()
    {
        
    }

    protected virtual void KnockBack(Vector3 knockBackVelocity)
    {
        
    }
}
