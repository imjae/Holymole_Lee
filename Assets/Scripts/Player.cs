using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // 죽음
    protected override void Die()
    {
        Debug.Log("죽음 !");

        // 실행중이던 애니메이션 트리거 전부 종료
        IsDie = true;
        IsAttacked = false;
        IsFalling = false;
        IsGrounded = false;
    }
}
