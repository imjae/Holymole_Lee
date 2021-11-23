using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{

    public static bool GroundCheck(
                this GameObject targetObject,
                Transform groundCheckPosition,
                float groundDistance
                )
    {
        bool result = true;

        result = Physics.CheckSphere(groundCheckPosition.position, groundDistance);

        return result;
    }

    public static bool GroundCheck(
                this GameObject targetObject,
                Transform groundCheckPosition,
                LayerMask checkLayer,
                float groundDistance = 1f
                )
    {
        bool result = true;

        result = Physics.CheckSphere(groundCheckPosition.position, groundDistance, checkLayer);

        return result;
    }
}