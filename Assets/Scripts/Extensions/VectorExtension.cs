using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector3 InputDirection(this Vector3 direction, Animator animator)
    {
        Vector3 result = Vector3.zero;

        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        result = Vector3.forward * inputVertical + Vector3.right * inputHorizontal;

        float percent;

        if (Input.GetAxisRaw("Vertical") < 0)
            percent = -1f * result.magnitude;
        else
            percent = 1f * result.magnitude;

        animator.SetFloat("RunPercent", percent, 0.1f, Time.deltaTime);

        return result.normalized;
    }

    public static Vector3 CalcJumpVelocity(this Vector3 velocity, float jumpHeight)
    {
        // 원하는 높이까지 점프하기위한 속력을 구하는 공식
        // v = 루트(h * -2 * g)
        Vector3 result = velocity;

        result.y = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);

        return result;
    }

    public static Vector3 CalcGravityVelocity(this Vector3 velocity)
    {
        // 낙하 속도 생성하는 공식
        // 1/2 * 중력 * 시간^2
        velocity.y = Physics.gravity.y * Time.deltaTime;

        return velocity;
    }
}