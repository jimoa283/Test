using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private RegisterWindow registerWindow;

    public override void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        registerWindow = GetComponentInChildren<RegisterWindow>();
        registerWindow.Init();

    }

    public override void OnEnter()
    {
        UIManager.Instance.PauseBeforePanel();
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        canvasGroup.interactable = false;
    }

    public override void OnResume()
    {
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
    }
}
