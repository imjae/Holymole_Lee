using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStreamingTrigger : MonoBehaviour
{
    [SerializeField]
    private string streamTargetScene;

    [SerializeField]
    private string triggerOwnScene;

    private IEnumerator StreamingTargetScene()
    {
        var targetScene = SceneManager.GetSceneByName(streamTargetScene);
        if(!targetScene.isLoaded)
        {
            // Additive �������� ������ �������� �ε�ɶ� ù��° ���� ��� ������Ʈ�� ����� ������ �߻��Ѵ�.
            var op = SceneManager.LoadSceneAsync(streamTargetScene, LoadSceneMode.Additive);
            
            
            while(!op.isDone)
            {
                yield return null;
            }
        }

    }

    private IEnumerator UnloadStreamingScene()
    {
        var targetScene = SceneManager.GetSceneByName(streamTargetScene);
        if(targetScene.isLoaded)
        {
            var currentScene = SceneManager.GetSceneByName(triggerOwnScene);
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("MoveToSceneObject"), currentScene);

            var op = SceneManager.UnloadSceneAsync(streamTargetScene);
            while(!op.isDone)
            {
                yield return null;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if(dir < 90f)
            {
                StartCoroutine(StreamingTargetScene());
            }
            else
            {
                StartCoroutine(UnloadStreamingScene());
            }
        }
    }

}
