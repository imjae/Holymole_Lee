using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // TODO Camera는 VR의 캐릭터가 될 예정
    // 카메라의 위치를 담고있는 리스트
    // 게임 실행 시점에서 Resources 폴더에 있는 카메라 위치 전부 읽어서 리스트에 저장
    public LinkedList<Transform> cameraTransformList;

    public LinkedListNode<Transform> currentNode;

    private Camera mainCamera;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Transform[] tmpArr = Resources.LoadAll<Transform>("CameraTransform");
        cameraTransformList = new LinkedList<Transform>(tmpArr);

        // 첫 번째 노드 초기화
        currentNode = cameraTransformList.First;
        mainCamera = Camera.main;

        TransferCamera(currentNode);
    }

    // 노드의 위치값을 카메라에 덮어쓰기
    private void TransferCamera(LinkedListNode<Transform> node)
    {
        mainCamera.transform.position = node.Value.position;
        mainCamera.transform.rotation = node.Value.rotation;
    }

    private LinkedListNode<Transform> NextNode()
    {
        currentNode = currentNode.Next;
        return currentNode;
    }

    private LinkedListNode<Transform> PreviousNode()
    {
        currentNode = currentNode.Previous;
        return currentNode;
    }

    public void NextCamera()
    {
        TransferCamera(NextNode());
    }
    public void PreviousCamera()
    {
        TransferCamera(PreviousNode());
    }
}
