using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStreamingTrigger : MonoBehaviour
{
    [SerializeField]
    private string streamTargetScene;

    private IEnumerator StreamingTargetScene()
    {

    }

    void OnTriggerExit(Collider other)
    {
        
    }

}
