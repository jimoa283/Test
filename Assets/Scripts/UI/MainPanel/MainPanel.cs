using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BasePanel
{
    private MainWindow mainWindow;
    private CanvasGroup canvasGroup;

    public override void Init()
    {        
        mainWindow = GetComponentInChildren<MainWindow>();
        mainWindow.Init();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        UIManager.Instance.PauseBeforePanel();
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
        mainWindow.ShowCurrentFolderContent();
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
        canvasGroup.interactable = false;
    }

    public override void OnPause()
    {
        //gameObject.SetActive(false);
        canvasGroup.interactable = false;
    }

    public override void OnResume()
    {
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
        mainWindow.ShowCurrentFolderContent();
    }
}
