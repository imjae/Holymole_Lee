using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // ����
    protected override void Die()
    {
        Debug.Log("���� !");

        // �������̴� �ִϸ��̼� Ʈ���� ���� ����
        IsDie = true;
        IsAttacked = false;
        IsFalling = false;
        IsGrounded = false;
    }
}
