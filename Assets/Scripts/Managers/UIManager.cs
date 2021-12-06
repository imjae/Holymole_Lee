using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;

public class UIManager : Singleton<UIManager>
{
    public GameObject ingameMenu;
    public GameObject printingText;
    public Button returnBtn, exitBtn;

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
    void Start()
    {
        ingameMenu = returnBtn.transform.parent.gameObject;
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
