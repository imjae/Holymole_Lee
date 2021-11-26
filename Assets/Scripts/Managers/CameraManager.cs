using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // TODO Camera�� VR�� ĳ���Ͱ� �� ����
    // ī�޶��� ��ġ�� ����ִ� ����Ʈ
    // ���� ���� �������� Resources ������ �ִ� ī�޶� ��ġ ���� �о ����Ʈ�� ����
    public LinkedList<Transform> cameraTransformList;

    public LinkedListNode<Transform> currentNode;

    private Camera mainCamera;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Transform[] tmpArr = Resources.LoadAll<Transform>("CameraTransform");
        cameraTransformList = new LinkedList<Transform>(tmpArr);

        // ù ��° ��� �ʱ�ȭ
        currentNode = cameraTransformList.First;
        mainCamera = Camera.main;

        TransferCamera(currentNode);
    }

    // ����� ��ġ���� ī�޶� �����
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
