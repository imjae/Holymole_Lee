using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonsterFactory<Monster>
{
    protected override Monster Create(Monster _type)
    {
        Monster monster = null;

        monster = Instantiate(_type).GetComponent<Monster>();

        return monster;
    }
}