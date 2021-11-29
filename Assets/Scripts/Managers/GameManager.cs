using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (Instance != this)
            Destroy(gameObject);
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
    }
}
