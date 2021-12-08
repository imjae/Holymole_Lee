using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingTrigger : MonoBehaviour
{
    //Obtacle�� ��Ȯ�� ���� ������ �մϴ�.
    //�̰��� �����ϰ� ��ü�ص� �Ǵ� ����.. ���� ����.. �̤�
    //������ ������ ������ 20�� 20��.. ������� �ð��̱�..
    //�ٵ� ���� �����ʴϴ�
    //�� ��ũ��Ʈ�� ����� ���� �𸣰�����
    //�ູ�� �Ϸ� �ǽʽÿ� ^^
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

