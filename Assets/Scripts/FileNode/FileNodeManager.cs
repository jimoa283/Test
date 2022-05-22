using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileNodeManager : MSingleton<FileNodeManager>
{
    public FolderFile rootFolder;                                //源文件夹
    //public Dictionary<string, BaseFile> fileNodeDic;          

    public FolderFile currentFolder;                            //现在打开的文件夹
    public OtherFile currentOtherFile;                         //现在打开的文件

    /*private void Awake()
    {
        fileContainer = Resources.Load<FileContainerSO>("SO/FileContainer");
        if (fileContainer.rootFile.fileName == "")
        {
            rootFolder = new FolderFile("Root");
            fileContainer.rootFile = rootFolder;
        }
        else
            rootFolder = fileContainer.rootFile;
        fileNodeDic = new Dictionary<string, BaseFile>();

        foreach (var file in fileContainer.fileList)
        {
            fileNodeDic.Add(file.path, file);
        }
    }*/

    protected override void Init()
    {
        /*fileContainer = Resources.Load<FileContainerSO>("SO/FileContainer");
        if (fileContainer.rootFile.fileName == "")
        {
            rootFolder = new FolderFile("Root");
            fileContainer.rootFile = rootFolder;
        }
        else
            rootFolder = fileContainer.rootFile;
        fileNodeDic = new Dictionary<string, BaseFile>();

        foreach (var file in fileContainer.fileList)
        {
            fileNodeDic.Add(file.path, file);
        }*/
        rootFolder = new FolderFile("Root");
        //fileNodeDic = new Dictionary<string, BaseFile>();
    }


    public int CreateFile(string fileName,FolderFile fatherFolder,int size,FileType fileType, string ownerAccountNumber, LimitType limitType)
    {
        if (fatherFolder.childeFileList.Find(x => x.fileName == fileName) != null)
            return -2;

        int startBlockIndex = BlockManager.Instance.RequestBlocks(size);

        if (startBlockIndex == -1)
            return -1;

        

        switch (fileType)
        {
            case FileType.Folder:
                FolderFile folder = new FolderFile(fatherFolder, fileName, size, startBlockIndex,ownerAccountNumber,limitType);
                fatherFolder.childeFileList.Add(folder);
                break;
            case FileType.Other:
                OtherFile file = new OtherFile(fatherFolder, fileName, size, startBlockIndex,ownerAccountNumber,limitType);
                fatherFolder.childeFileList.Add(file);
                break;
            default:
                break;
        }

        return 1;
    }

    public void DeleteFile(BaseFile file)
    {
        switch (file.fileType)
        {
            case FileType.Folder:
                DeleteFolder(file as FolderFile);
                break;
            case FileType.Other:
                DeleteOtherFile(file as OtherFile);
                break;
            default:
                break;
        }
    }

    public void DeleteAllFile()
    {
        while (rootFolder.childeFileList.Count > 0)
        {
            DeleteFile(rootFolder.childeFileList[0]);
        }

        currentFolder = rootFolder;
        currentOtherFile = null;
    }

    private void DeleteFolder(FolderFile folder)
    {

        folder.fatherFile.childeFileList.Remove(folder);
        folder.fatherFile = null;

        //fileContainer.fileList.Remove(folder);
        //fileNodeDic.Remove(folder.path);

        BlockManager.Instance.ReleaseBlock(folder.indexNode.startBlockIndex);

        /*foreach(var file in folder.childeFileList)
        {
            DeleteFile(file);
        }*/

        while(folder.childeFileList.Count>0)
        {
            DeleteFile(folder.childeFileList[0]);
        }
    }

    private void DeleteOtherFile(OtherFile otherFile)
    {
        otherFile.fatherFile.childeFileList.Remove(otherFile);
        otherFile.fatherFile = null;

        //fileContainer.fileList.Remove(otherFile);
        //fileNodeDic.Remove(otherFile.path);

        BlockManager.Instance.ReleaseBlock(otherFile.indexNode.startBlockIndex);
    }

/*    public BaseFileNode GetFileNodeByName(BaseFileNode node,string fileName)
    {
        BaseFileNode childNode = node.GetChildFileByName(fileName);
        if (childNode != null) return childNode;

        for (int i = 0; i < node.childNode.Count; i++)
        {
            childNode = GetFileNodeByName(node.childNode[i], fileName);
            if (childNode != null) return childNode;
        }

        return null;
    }*/

/* public void SetUserRootFile(UserData userData)
    {
        RootFileNode rootFile = rootFileNodes.Find(x => x.ownerNumber == userData.AccountNumber);
        if (rootFile==null)
        {
            RootFileNode rootFileNode = new RootFileNode(userData.AccountNumber);
            userData.RootFileNode = rootFileNode;
            return;
        }

        userData.RootFileNode = rootFile;
    }*/

}



