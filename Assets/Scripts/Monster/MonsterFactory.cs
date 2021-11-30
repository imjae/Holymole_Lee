using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterFactory<T> : MonoBehaviour
{
    public Monster Spawn(T _type, Transform _parent)
    {
        Monster monster = this.Create(_type);
        monster.transform.SetParent(_parent, false);
        monster.transform.localPosition = Vector3.zero;
        return monster;
    }

    protected abstract Monster Create(T _type);
}
