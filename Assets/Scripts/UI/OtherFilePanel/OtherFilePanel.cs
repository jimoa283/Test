using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherFilePanel : BasePanel
{
    private OtherFileWindow otherFileWindow;
    private CanvasGroup canvasGroup;

    public override void Init()
    {
        otherFileWindow = GetComponentInChildren<OtherFileWindow>();
        canvasGroup = GetComponent<CanvasGroup>();
        otherFileWindow.Init();
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
        otherFileWindow.Show();
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
        canvasGroup.interactable = false;
    }

    public override void OnPause()
    {
        //gameObject.SetActive(true);
        canvasGroup.interactable = false;
    }

    public override void OnResume()
    {
        gameObject.SetActive(true);
        canvasGroup.interactable = true;
        //otherFileWindow.Show();
    }
}
