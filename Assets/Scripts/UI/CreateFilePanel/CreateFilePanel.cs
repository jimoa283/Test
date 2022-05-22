using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFilePanel : BasePanel
{
    public CreateFileWindow createFileWindow;

    public override void Init()
    {
        createFileWindow = GetComponentInChildren<CreateFileWindow>();
        createFileWindow.Init();
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        createFileWindow.ResetWindow();
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        createFileWindow.ResetWindow();
        gameObject.SetActive(true);
    }
}
