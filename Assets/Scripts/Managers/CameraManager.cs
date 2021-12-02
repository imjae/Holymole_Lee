using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    // TODO Camera�� VR�� ĳ���Ͱ� �� ����
    // ī�޶��� ��ġ�� ����ִ� ����Ʈ
    // ���� ���� �������� Resources ������ �ִ� ī�޶� ��ġ ���� �о ����Ʈ�� ����
    public LinkedList<Transform> cameraTransformList;

    public LinkedListNode<Transform> currentNode;
    // public GameObject xrOrigin;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        Transform[] tmpArr = Resources.LoadAll<Transform>("CameraTransform");
        cameraTransformList = new LinkedList<Transform>(tmpArr);

        if (currentNode == null)
        {
            // ù ��° ��� �ʱ�ȭ
            currentNode = cameraTransformList.First;

            TransferCamera(currentNode);
        }
    }

    // ����� ��ġ���� ī�޶� �����
    private void TransferCamera(LinkedListNode<Transform> node)
    {
        Debug.Log(node.Value.name);
        mainCamera.transform.position = node.Value.position;
        mainCamera.transform.rotation = node.Value.rotation;
        // xrOrigin.transform.position = node.Value.position;
        // xrOrigin.transform.rotation = node.Value.rotation;
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

    public void NextCamera(int _step)
    {
        for (int i = 0; i < _step; i++)
        {
            TransferCamera(NextNode());
        }
    }
    public void PreviousCamera(int _step)
    {
        for(int i=0;i<_step;i++)
        {
            TransferCamera(PreviousNode());
        }
    }
}
