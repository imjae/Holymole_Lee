using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingTrigger : MonoBehaviour
{
    //Obtacle과 정확히 같은 역할을 합니다.
    //이것을 삭제하고 대체해도 되는 상태.. 현재 상태.. ㅜㅜ
    //하지만 지금은 수요일 20시 20분.. 운명적인 시간이군..
    //다들 수고 많으십니다
    //이 스크립트를 열어보실 지는 모르겠지만
    //행복한 하루 되십시오 ^^
    public Transform respawnSpot;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mole = other.GetComponent<Mole>();
            mole.controller.enabled = false;
            other.transform.position = respawnSpot.transform.position;
            mole.controller.enabled = true;
            UIManager.Instance.FadeOn();
            StartCoroutine(IsDying(mole));
        }
    }

    IEnumerator IsDying(Player player)
    {
        player.IsDie = true;
        yield return new  WaitForSeconds(3f);
        player.IsDie = false;
    }
}

