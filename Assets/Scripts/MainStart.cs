using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStart : MonoBehaviour
{
  
    void Start()
    {
        Init();
    }

    public void Init()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.LoginPanel));
    }

}
