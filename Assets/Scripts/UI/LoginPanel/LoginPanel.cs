using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : BasePanel
{
    private LoginWindow loginWindow;
    private CanvasGroup canvasGroup;

    public override void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        loginWindow = transform.GetChildTransform("LoginWindow").GetComponent<LoginWindow>();
        loginWindow.Init();
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        loginWindow.ResetWindow();
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        loginWindow.ResetWindow();
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        gameObject.SetActive(true);
    }
}
