using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum UIPanelType
{
    LoginPanel,
    RegisterPanel,
    MainPanel,
    BitMapPanel,
    CreateFilePanel,
    OtherFilePanel
}



public class UIManager : Singleton<UIManager>
{

    private Dictionary<UIPanelType, string> panelPathDic;
    private Dictionary<UIPanelType, BasePanel> panelDic;

    private Stack<BasePanel> panelStack;

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    public Stack<BasePanel> PanelStack { get => panelStack; }

    public UIManager()
    {
        panelDic = new Dictionary<UIPanelType, BasePanel>();
        panelPathDic = new Dictionary<UIPanelType, string>();
        panelStack = new Stack<BasePanel>();
        ParseUIPanelJson();
    }

    public void SetSimpleWindow(string text,Action action)
    {
        PauseBeforePanel();
        SimpleWindow simpleWindow = PoolManager.Instance.GetObj("SimpleWindow").GetComponent<SimpleWindow>();
        simpleWindow.transform.SetParent(CanvasTransform,false);
        simpleWindow.transform.localPosition = Vector2.zero;
        simpleWindow.Init(text, action);
    }

    private void ParseUIPanelJson()
    {
        TextAsset textUIPanel = Resources.Load<TextAsset>("JSON/panelJson");
        JSONObject jSONObject = new JSONObject(textUIPanel.text);
        foreach(var obj in jSONObject.list)
        {
            string panelName = obj["UIPanelType"].str;
            string path = obj["path"].str;
            UIPanelType uIPanelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelName);
            panelPathDic.Add(uIPanelType, path);
        }
    }

    public BasePanel GetPanel(UIPanelType type)
    {
        if(!panelDic.ContainsKey(type))
        {
            BasePanel panel = GameObject.Instantiate(Resources.Load<GameObject>(panelPathDic[type])).GetComponent<BasePanel>();         
            panel.transform.SetParent(CanvasTransform, false);
            panel.Init();
            panelDic.Add(type, panel);
        }

        return panelDic[type];
    }

    /// <summary>
    /// 清空当前的字典，用于场景转换
    /// </summary>
    public void ClearDic()
    {
        PopAllPanel();
        if (panelDic.Count > 0)
        {
            foreach (var panel in panelDic)
            {
                GameObject.Destroy(panel.Value.gameObject);
            }
        }
        panelDic.Clear();
    }

    /// <summary>
    /// 把panel放到panel栈里
    /// </summary>
    /// <param name="panel"></param>
    public void PushPanel(BasePanel panel)
    {
       /* if (panelStack == null)
            panelStack = new Stack<BasePanel>();*/

        if (panelStack.Contains(panel))
            return;
        
        panel.OnEnter();
        panelStack.Push(panel);       
    }

    /// <summary>
    /// 把当期的panel出栈退出，如有上一级panel则恢复其操控
    /// </summary>
    public void PopPanel(UIPanelType uIPanelType)
    {
        if(panelStack.Count==0)
            return;
        if (panelDic.ContainsKey(uIPanelType) && panelStack.Peek() == panelDic[uIPanelType])
        {
            panelStack.Pop().OnExit();
            if (panelStack.Count > 0)
                panelStack.Peek().OnResume();
        }
    }

    /// <summary>
    /// 将全部的panel出栈，用于场景转换
    /// </summary>
    public void PopAllPanel()
    {
        while(panelStack.Count>0)
        {
            panelStack.Pop().OnExit();
        }
    }

    public void PauseBeforePanel()
    {
        if(panelStack.Count>0)
           panelStack.Peek().OnPause();
    }

    public BasePanel GetTopPanel()
    {
        if (panelStack.Count > 0)
            return panelStack.Peek();

        return null;
    }

}

