using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FileObj : MonoBehaviour
{
    private Image fileImage;
    public BaseFile baseFile;
    private Text fileNameText;
    private Text fileLimitTypeText;
    private Text fileSizeText;
    //private Image backGroundImage;
    private MainWindow mainWindow;
    private Button button;

    public Sprite otherSprite;
    public Sprite folderSprite;

    public void Init(BaseFile baseFile,MainWindow mainWindow)
    {
        this.baseFile = baseFile;
        this.mainWindow = mainWindow;
        fileNameText = GetComponentInChildren<Text>();
        fileImage = transform.GetChildTransform("FileImage").GetComponent<Image>();
        fileLimitTypeText = transform.GetChildTransform("FileLimitType").GetComponent<Text>();
        fileSizeText = transform.GetChildTransform("FileSize").GetComponent<Text>();
        button = GetComponent<Button>();

        fileNameText.text= baseFile.fileName;


        switch (baseFile.fileType)
        {
            case FileType.Folder:
                fileImage.sprite = folderSprite;
                break;
            case FileType.Other:
                fileImage.sprite = otherSprite;
                break;
            default:
                break;
        }

        switch (baseFile.indexNode.limitType)
        {
            case LimitType.Free:
                fileLimitTypeText.text = "无限制";
                break;
            case LimitType.Middle:
                fileLimitTypeText.text = "其他用户只读";
                break;
            case LimitType.High:
                fileLimitTypeText.text = "其他用户无法访问";
                break;
            default:
                break;
        }

        fileSizeText.text = baseFile.indexNode.specificSize+"KB";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (mainWindow.selectFileObj != this)
            {
                mainWindow.selectFileObj = this;
            }               
            else
                OpenFile();
        });
    }

    public void OpenFile()
    {
        if(baseFile.indexNode.limitType==LimitType.High)
        {
            if (UserDataManager.Instance.CurrentUser.AccountNumber != baseFile.indexNode.ownerAccountNumber
                    && UserDataManager.Instance.CurrentUser.UserType != UserType.Manager)
            {
                UIManager.Instance.SetSimpleWindow("没有权限", () => UIManager.Instance.GetPanel(UIPanelType.MainPanel).OnResume());
                return;
            }
        }
        

        switch (baseFile.fileType)
        {
            case FileType.Folder:
                //FileNodeManager.Instance.currentFolder = baseFile as FolderFile;
                baseFile.OpenFile();
                mainWindow.selectFileObj = null;
                mainWindow.ShowCurrentFolderContent();
                break;
            case FileType.Other:
                //FileNodeManager.Instance.currentOtherFile = baseFile as OtherFile;
                baseFile.OpenFile();
                mainWindow.selectFileObj = null;
                UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.OtherFilePanel));
                break;
            default:
                break;
        }
    }

    
}
