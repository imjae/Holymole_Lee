using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private bool _isDash;

    public bool IsDash
    {
        get => _isDash;
        set => _isDash = value;
    }
}
