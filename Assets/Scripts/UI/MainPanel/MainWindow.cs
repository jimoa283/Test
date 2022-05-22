using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour
{
    [SerializeField]private List<FileObj> fileObjList = new List<FileObj>();
    private Transform fileObjContent;
    //private InputField filePathInputField;
    //private Button enterButton;
    private Button bitMapButton;
    private Text currentUserText;
    private Text currentUserTypeText;
    public GameObject fileObjPrefab;
    public FileObj selectFileObj;
    private Text currentFolderPath;
    private Button createButton;
    private Button deleteButton;
    private Button returnButton;
    private Button returnRootButton;
    private Button exitButton;
    private Button deleteAllButton;

    public void Init()
    {
        FileNodeManager.Instance.currentFolder = FileNodeManager.Instance.rootFolder;
        bitMapButton = transform.GetChildTransform("BitMapButton").GetComponent<Button>();
        currentUserText = transform.GetChildTransform("CurrentUser").GetComponent<Text>();
        currentUserTypeText = transform.GetChildTransform("CurrentUserType").GetComponent<Text>();
        createButton = transform.GetChildTransform("CreateButton").GetComponent<Button>();
        deleteButton = transform.GetChildTransform("DeleteButton").GetComponent<Button>();
        returnButton = transform.GetChildTransform("ReturnFatherButton").GetComponent<Button>();
        returnRootButton = transform.GetChildTransform("ReturnRootButton").GetComponent<Button>();
        exitButton = transform.GetChildTransform("ExitButton").GetComponent<Button>();
        deleteAllButton = transform.GetChildTransform("DeleteAllButton").GetComponent<Button>();
        fileObjContent = transform.GetChildTransform("Content");
        currentFolderPath = transform.GetChildTransform("CurrentFolderPath").GetComponent<Text>();

        bitMapButton.onClick.RemoveAllListeners();
        bitMapButton.onClick.AddListener(() =>
        {
            UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.BitMapPanel));
        });

        

        createButton.onClick.AddListener(CreateFile);
        deleteButton.onClick.AddListener(DeleteFile);
        returnButton.onClick.AddListener(ReturnBeforFolder);
        returnRootButton.onClick.AddListener(ReturnRootFoler);
        deleteAllButton.onClick.AddListener(DeleteAllFile);

        exitButton.onClick.AddListener(() =>
        {
            UIManager.Instance.PopPanel(UIPanelType.MainPanel);
        });
    }

    public void ShowCurrentFolderContent()
    {
        selectFileObj = null;

        foreach(var obj in fileObjList)
        {
            obj.gameObject.SetActive(false);
        }

        currentUserText.text = UserDataManager.Instance.CurrentUser.AccountNumber;
        currentUserTypeText.text = UserDataManager.Instance.CurrentUser.UserType.ToString();

        int num = FileNodeManager.Instance.currentFolder.childeFileList.Count;

        int i ;

        for(i=0;i<fileObjList.Count;i++)
        {
            if (num <= i)
            {
                fileObjList[i].gameObject.SetActive(false);
            }
            else
            {
                fileObjList[i].gameObject.SetActive(true);
                fileObjList[i].Init(FileNodeManager.Instance.currentFolder.childeFileList[i],this);
            }
        }

        if(num>i)
        {
            for(;i<num;i++)
            {
                FileObj fileObj = Instantiate(fileObjPrefab, fileObjContent).GetComponent<FileObj>();
                fileObj.Init(FileNodeManager.Instance.currentFolder.childeFileList[i],this);
                fileObjList.Add(fileObj);
            }
        }

        currentFolderPath.text = FileNodeManager.Instance.currentFolder.path;
    }

  /*  public void OpenFile()
    {
        
    }*/

    public void DeleteFile()
    {
        if (selectFileObj== null)
            return;

        if(FileNodeManager.Instance.currentFolder.indexNode.limitType==LimitType.High||FileNodeManager.Instance.currentFolder.indexNode.limitType==LimitType.Middle)
        {
            if (UserDataManager.Instance.CurrentUser.AccountNumber != FileNodeManager.Instance.currentFolder.indexNode.ownerAccountNumber
                    && UserDataManager.Instance.CurrentUser.UserType != UserType.Manager)
            {
                UIManager.Instance.SetSimpleWindow("没有权限", () => UIManager.Instance.GetPanel(UIPanelType.MainPanel).OnResume());
                return;
            }
        }

        if (selectFileObj.baseFile.indexNode.limitType == LimitType.High||selectFileObj.baseFile.indexNode.limitType==LimitType.Middle)
        {
            if (UserDataManager.Instance.CurrentUser.AccountNumber != FileNodeManager.Instance.currentOtherFile.indexNode.ownerAccountNumber
                    && UserDataManager.Instance.CurrentUser.UserType != UserType.Manager)
            {
                UIManager.Instance.SetSimpleWindow("没有权限", () => UIManager.Instance.GetPanel(UIPanelType.MainPanel).OnResume());
                return;
            }
        }

        FileNodeManager.Instance.DeleteFile(selectFileObj.baseFile);
        selectFileObj = null;
        ShowCurrentFolderContent();
    }

    public void DeleteAllFile()
    {
        if (UserDataManager.Instance.CurrentUser.UserType != UserType.Manager)
            return;

        FileNodeManager.Instance.DeleteAllFile();
        selectFileObj = null;

        ShowCurrentFolderContent();
    }

    public void CreateFile()
    {
        if (selectFileObj != null)
            selectFileObj = null;

        if (FileNodeManager.Instance.currentFolder.indexNode.limitType == LimitType.High || FileNodeManager.Instance.currentFolder.indexNode.limitType == LimitType.Middle)
        {
            if (UserDataManager.Instance.CurrentUser.AccountNumber != FileNodeManager.Instance.currentFolder.indexNode.ownerAccountNumber
                    && UserDataManager.Instance.CurrentUser.UserType != UserType.Manager)
            {
                UIManager.Instance.SetSimpleWindow("没有权限", () => UIManager.Instance.GetPanel(UIPanelType.MainPanel).OnResume());
                return;
            }
        }

        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.CreateFilePanel));
    }

    public void ReturnBeforFolder()
    {
        if (FileNodeManager.Instance.currentFolder == FileNodeManager.Instance.rootFolder)
            return;

        if (selectFileObj != null)
            selectFileObj = null;

        FileNodeManager.Instance.currentFolder = FileNodeManager.Instance.currentFolder.fatherFile;

        ShowCurrentFolderContent();
    }

    public void ReturnRootFoler()
    {
        if (FileNodeManager.Instance.currentFolder == FileNodeManager.Instance.rootFolder)
            return;

        if (selectFileObj != null)
            selectFileObj = null;

        FileNodeManager.Instance.currentFolder = FileNodeManager.Instance.rootFolder;

        ShowCurrentFolderContent();
    }
}
