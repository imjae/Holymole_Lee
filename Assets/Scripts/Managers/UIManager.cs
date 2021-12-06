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
    public static GameObject ingameMenu;  
    public GameObject printingText;
    public Button returnBtn, exitBtn;

    //
    // Menu Open, Close  
    public static void OpenMenu()
    {
        ingameMenu.SetActive(true);
    }
    public static void CloseMenu()
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
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(ingameMenu);
        DontDestroyOnLoad(printingText);
    }
    void update()
    {

    }
}
