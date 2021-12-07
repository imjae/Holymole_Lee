using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _isOpened = false;

    public bool IsOpened
    {
        get => _isOpened;
        set {
            _isOpened = value;

            if(gameObject.TryGetComponent<Collider>(out Collider collider))
            {
                collider.enabled = !_isOpened;
            }
        }
    }
}
