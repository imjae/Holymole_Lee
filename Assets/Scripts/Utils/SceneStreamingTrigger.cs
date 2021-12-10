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
            // Additive �������� ������ �������� �ε�ɶ� ù��° ���� ��� ������Ʈ�� ����� ������ �߻��Ѵ�.
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
    
            // TODO ������ �� ��ε�� �ڵ���Ʈ�ѷ� ���̰� ���峪����
            var op = SceneManager.UnloadSceneAsync(streamTargetScene);
            while (!op.isDone)
            {
                yield return null;
            }

            // �� ��ε� �Ǹ鼭 Ray Interactor�� interaction manager�� ���ǵǴ� ���� ������ ��Ȱ��ȭ ������
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
