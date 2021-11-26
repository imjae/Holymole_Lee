using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // TODO Camera는 VR의 캐릭터가 될 예정
    // 카메라의 위치를 담고있는 리스트
    // 게임 실행 시점에서 Resources 폴더에 있는 카메라 위치 전부 읽어서 리스트에 저장
    public LinkedList<Transform> CameraTransformList;

    public Transform current;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
