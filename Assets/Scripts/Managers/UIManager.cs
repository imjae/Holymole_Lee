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
    public GameObject ingameMenu, printingText, fadeInNOut;
    private TextMeshProUGUI temp;
    private float fadeTime;
    private bool isPause;

    //
    // Menu Open, Close  
    public void OpenMenu()
    {
        ingameMenu.SetActive(true);
        PauseOnOff();
    }
    public void CloseMenu()
    {
        ingameMenu.SetActive(false);
        PauseOnOff();
    }
    public void Exit()
    {
        Application.Quit();
    }

    // pause
    public void PauseOnOff()
    {
        if(isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
        }
        else if(isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            CancelInvoke();
        }
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
    public void PrintText(List<string> txtList, float durationTime)
    {
        StartCoroutine(PrintTextCouroutine(txtList, durationTime));
    }
    IEnumerator PrintTextCouroutine(List<string> txtList, float durationTime) //주로 스테이지에 진입했을 때 사용
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

    //
    // FadeIn and Out
    IEnumerator FadeEffect()
    {        
        float fadeCount = 1f;
        if(fadeInNOut != null)
        {
            fadeInNOut.SetActive(true);
            while(fadeCount > 0)
            {
                fadeCount -= 0.005f;
                fadeInNOut.GetComponent<Image>().color = new Color(0,0,0,fadeCount);
                yield return new WaitForSeconds(0.01f);
            }
            yield return null;
        }
    }
    public void FadeOn()
    {
        StartCoroutine(FadeEffect());
    }
    void Start()
    {
        StartCoroutine(FadeEffect());
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
