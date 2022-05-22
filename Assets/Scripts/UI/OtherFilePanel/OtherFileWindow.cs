using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherFileWindow : MonoBehaviour
{
    private InputField otherFileInputField;
    private Button exitButton;

   public void Init()
    {
        otherFileInputField = GetComponentInChildren<InputField>();
        exitButton = transform.GetChildTransform("ExitButton").GetComponent<Button>();

        otherFileInputField.onValueChanged.AddListener(ReadContent);
        exitButton.onClick.AddListener(() =>UIManager.Instance.PopPanel(UIPanelType.OtherFilePanel) );
    }

    public void Show()
    {
        otherFileInputField.text = FileNodeManager.Instance.currentOtherFile.content;
    }

    public void ReadContent(string value)
    {
        if (value == FileNodeManager.Instance.currentOtherFile.content)
            return;

        switch (FileNodeManager.Instance.currentOtherFile.indexNode.limitType)
        {
            case LimitType.Free:
                break;
            case LimitType.Middle:
                if (UserDataManager.Instance.CurrentUser.AccountNumber != FileNodeManager.Instance.currentOtherFile.indexNode.ownerAccountNumber
                    && UserDataManager.Instance.CurrentUser.UserType != UserType.Manager)
                {
                    otherFileInputField.text = FileNodeManager.Instance.currentOtherFile.content;
                    UIManager.Instance.SetSimpleWindow("没有权限",()=> UIManager.Instance.GetPanel(UIPanelType.OtherFilePanel).OnResume());
                    return;
                }
                break;
            case LimitType.High:
                break;
            default:
                break;
        }

        FileNodeManager.Instance.currentOtherFile.content = value;
    }
}
