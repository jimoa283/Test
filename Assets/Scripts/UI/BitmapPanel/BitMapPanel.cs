using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitMapPanel : BasePanel
{
    private BitMapWindow bitMapWindow;


    public override void Init()
    {
        bitMapWindow = GetComponentInChildren<BitMapWindow>();
        bitMapWindow.Init();
    }

    public override void OnEnter()
    {
        UIManager.Instance.PauseBeforePanel();
        UIManager.Instance.GetPanel(UIPanelType.MainPanel).gameObject.SetActive(false);
        bitMapWindow.Show();
        gameObject.SetActive(true);
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
        gameObject.SetActive(true);
        bitMapWindow.Show();
    }
}
