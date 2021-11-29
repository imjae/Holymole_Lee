using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : Singleton<TextManager>
{
    public GameObject printingText;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(printingText);
    }
}
