using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SimpleWindow : MonoBehaviour
{
    private Text tipText;
    private Button button;
    private Action action;

     public void Init(string text,Action action)
    {
        tipText = transform.GetChildTransform("MainText").GetComponent<Text>();
        button = transform.GetComponentInChildren<Button>();
        this.action = action;

        tipText.text = text;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(DoButtonAction);
    }

    private void DoButtonAction()
    {
        Debug.Log("OH");
        action?.Invoke();
        PoolManager.Instance.PushObj("SimpleWindow", gameObject);
    }
}
