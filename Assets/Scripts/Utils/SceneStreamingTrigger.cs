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
        if (!targetScene.isLoaded)
        {
            // Additive 설정하지 않으면 다음씬이 로드될때 첫번째 씬의 모든 오브젝트가 사라져 문제가 발생한다.
            var op = SceneManager.LoadSceneAsync(streamTargetScene, LoadSceneMode.Additive);


            while (!op.isDone)
            {
                yield return null;
            }
        }
        //          Debug.Log(FindObjectsOfType<Camera>().Length);

    }

    private IEnumerator UnloadStreamingScene()
    {
        var targetScene = SceneManager.GetSceneByName(streamTargetScene);
        if (targetScene.isLoaded)
        {
            var currentScene = SceneManager.GetSceneByName(triggerOwnScene);
            SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("MoveToSceneObject"), currentScene);
            SceneManager.MoveGameObjectToScene(MasterController.Instance.xrInteractionManager , currentScene);
    
            // TODO 지나간 맵 언로드시 핸드컨트롤러 레이가 고장나버림
            var op = SceneManager.UnloadSceneAsync(streamTargetScene);
            while (!op.isDone)
            {
                yield return null;
            }

            // 씬 언로드 되면서 Ray Interactor의 interaction manager가 유실되는 문제 때문에 재활성화 시켜줌
            MasterController.Instance.RightObtacleInteractor.enabled = false;
            MasterController.Instance.RightObtacleInteractor.enabled = true;
            MasterController.Instance.RightPuzzleInteractor.enabled = false;
            MasterController.Instance.RightPuzzleInteractor.enabled = true;

            MasterController.Instance.LeftObtacleInteractor.enabled = false;
            MasterController.Instance.LeftObtacleInteractor.enabled = true;
            MasterController.Instance.LeftPuzzleInteractor.enabled = false;
            MasterController.Instance.LeftPuzzleInteractor.enabled = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if (dir < 90f)
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
