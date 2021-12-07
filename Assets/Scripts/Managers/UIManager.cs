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
    public void PrintText(string txt, float durationTime) //내용, 지속시간
    {
        printingText.transform.parent.gameObject.SetActive(true);
        temp = printingText.GetComponent<TextMeshProUGUI>();
        temp.text = txt.ToString();
        Invoke("DisableText", durationTime);
    }
    public void DisableText()
    {
        printingText.transform.parent.gameObject.SetActive(false);
    }

    void update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            ingameMenu.SetActive(!ingameMenu.activeSelf);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            ingameMenu.SetActive(false);
        }
    }
}
