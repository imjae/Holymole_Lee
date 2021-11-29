using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    // TODO Camera는 VR의 캐릭터가 될 예정
    // 카메라의 위치를 담고있는 리스트
    // 게임 실행 시점에서 Resources 폴더에 있는 카메라 위치 전부 읽어서 리스트에 저장
    public LinkedList<Transform> cameraTransformList;

    public LinkedListNode<Transform> currentNode;

    private Camera mainCamera;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (Instance != this)
            Destroy(gameObject);
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
    }

    void Start()
    {
        Transform[] tmpArr = Resources.LoadAll<Transform>("CameraTransform");
        cameraTransformList = new LinkedList<Transform>(tmpArr);

        if (currentNode == null)
        {
            // 첫 번째 노드 초기화
            currentNode = cameraTransformList.First;
            mainCamera = Camera.main;

            TransferCamera(currentNode);
        }

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
