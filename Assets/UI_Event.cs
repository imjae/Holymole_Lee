using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Event : MonoBehaviour
{
    public Button returnBtn, exitBtn;
    
    public void ReturnBtn()
    {
        gameObject.SetActive(false);
        returnBtn.onClick.AddListener(
        delegate
        {
            gameObject.SetActive(false);
        });
    }
    public void ExitBtn()
    {
            Application.Quit();
        exitBtn.onClick.AddListener(
        delegate
        {
            Application.Quit();
        });
    }
}
