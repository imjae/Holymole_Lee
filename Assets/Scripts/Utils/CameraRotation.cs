using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    float rotX;
    float rotY;
    float sensitivity = 400f;
    float clampAngle = 40f;
    private static CameraRotation _instance;

    public static CameraRotation Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ�.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(CameraRotation)) as CameraRotation;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (Instance != this)
            Destroy(gameObject);
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
    }


    void Update()
    {
        // ī�޶��� ȸ��
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
}
