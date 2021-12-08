using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject mole;
    public GameObject xrRig;

    public GameObject moveToSceneObject;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        mole = FindObjectOfType<Mole>().gameObject;
        xrRig = FindObjectOfType<MasterController>().gameObject;
    }

}
