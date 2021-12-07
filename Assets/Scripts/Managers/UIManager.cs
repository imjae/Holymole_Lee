using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    public GameObject ingameMenu;
    public GameObject printingText;
    private TextMeshProUGUI temp;

    //
    // Menu Open, Close  
    public void OpenMenu()
    {
        ingameMenu.SetActive(true);
    }
    public void CloseMenu()
    {
        ingameMenu.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }

    //
    // Print
    public void PrintText(string txt, float durationTime) //주로 트리거에 부딪혔을 때 사용
    {
        printingText.transform.parent.gameObject.SetActive(true);
        temp = printingText.GetComponent<TextMeshProUGUI>();
        temp.text = txt.ToString();
        Invoke("DisableTextPanel", durationTime);
    }
    IEnumerator PrintText(List<string> txtList, float durationTime) //주로 스테이지에 진입했을 때 사용
    {
        float sentencePrintingTime = durationTime/txtList.Count; // 문장당 출력시간
        printingText.transform.parent.gameObject.SetActive(true);
        temp = printingText.GetComponent<TextMeshProUGUI>();
        for(int i=0; i < txtList.Count; i++)
        {
            temp.text = txtList[i];
            if(sentencePrintingTime * i <= durationTime)
            yield return new WaitForSeconds(sentencePrintingTime);
        }
        DisableTextPanel();
    }
    public void DisableTextPanel()
    {
        printingText.transform.parent.gameObject.SetActive(false);
        CancelInvoke();
    }

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            Debug.Log("메뉴 !");
            ingameMenu.SetActive(!ingameMenu.activeSelf);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("취소 !");
            ingameMenu.SetActive(false);
        }
    }
}
