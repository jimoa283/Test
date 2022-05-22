using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFileWindow : MonoBehaviour
{
    private InputField fileNameInputField;
    private Dropdown fileTypeDD;
    private Dropdown fileLimitTypeDD;
    private Button createButton;
    private Button exitButton;
    private Text errorText;

    [SerializeField] private string fileName;
    [SerializeField] private FileType fileType;
    [SerializeField] private LimitType limitType;

   public void Init()
    {
        fileNameInputField = GetComponentInChildren<InputField>();
        fileTypeDD = transform.GetChildTransform("FileType").GetComponent<Dropdown>();
        fileLimitTypeDD = transform.GetChildTransform("FileLimitType").GetComponent<Dropdown>();
        createButton = transform.GetChildTransform("CreateButton").GetComponent<Button>();
        exitButton = transform.GetChildTransform("ExitButton").GetComponent<Button>();
        errorText = transform.GetChildTransform("ErrorText").GetComponent<Text>();

        fileNameInputField.onValueChanged.AddListener(x => fileName = x);
        fileTypeDD.onValueChanged.AddListener(x =>
        {
           switch(x)
            {
                case 0:
                    fileType = FileType.Folder;
                    break;
                case 1:
                    fileType = FileType.Other;
                    break;
                default:
                    break;
            }
        });

        fileLimitTypeDD.onValueChanged.AddListener(x =>
        {
            switch(x)
            {
                case 0:
                    limitType = LimitType.Free;
                    break;
                case 1:
                    limitType = LimitType.Middle;
                    break;
                case 2:
                    limitType = LimitType.High;
                    break;
                default:
                    break;
            }
        });

        errorText.gameObject.SetActive(false);
        createButton.onClick.AddListener(CreateFile);
        exitButton.onClick.AddListener(() =>UIManager.Instance.PopPanel(UIPanelType.CreateFilePanel));
    }

    public void ResetWindow()
    {
        fileNameInputField.text = "";
        fileName = "";
        fileLimitTypeDD.value = 0;
        errorText.gameObject.SetActive(false);
        fileLimitTypeDD.value = 0;
    }

    private void CreateFile()
    {
        errorText.gameObject.SetActive(false);
        int temp = FileNodeManager.Instance.CreateFile(fileName, FileNodeManager.Instance.currentFolder, 1,
                      fileType, UserDataManager.Instance.CurrentUser.AccountNumber, limitType);

        switch (temp)
        {
            case -1:
                errorText.gameObject.SetActive(true);
                errorText.text = "磁盘空间不够";
                break;
            case -2:
                errorText.gameObject.SetActive(true);
                errorText.text = "文件名重复";
                break;
            case 1:
                UIManager.Instance.PopPanel(UIPanelType.CreateFilePanel);
                break;
            default:
                break;
        }
    }
}
