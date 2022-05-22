using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseFile 
{
    public string fileName;           //文件名
    public string path;              //文件路径
    public FolderFile fatherFile;    //父文件夹
    public FileType fileType;        //文件类型

    public IndexNode indexNode;       //索引节点

    public BaseFile()
    {

    }

    public BaseFile(FolderFile fatherFile, string fileName,int size,int startBlockIndex,string ownerAccountNumber, LimitType limitType)
    {
        this.fileName = fileName;
        this.fatherFile = fatherFile;
        path = fatherFile.path + "/" + fileName;
        indexNode = new IndexNode(size, startBlockIndex, ownerAccountNumber, limitType);
    }

    public virtual void OpenFile()
    {

    }


}

public class IndexNode
{
    public int size;                     //文件所占盘块数
    public int startBlockIndex;         //文件物理地址
    public LimitType limitType;         //文件保护限制类型
    public string ownerAccountNumber;   //创建者账号
    public int specificSize;            //文件具体大小

    public IndexNode(int size, int startBlockIndex, string ownerAccountNumber, LimitType limitType)
    {
        this.size = size;
        this.startBlockIndex = startBlockIndex;
        this.ownerAccountNumber = ownerAccountNumber;
        this.limitType = limitType;
        specificSize = 1;
    }
}

 [System.Serializable]
public class FolderFile:BaseFile
{
    public List<BaseFile> childeFileList = new List<BaseFile>();        //子文件集合

    public FolderFile(string fileName)
    {
        this.fileName = fileName;
        path = fileName;
        indexNode = new IndexNode(0, 0, "", LimitType.Free);
        fileType = FileType.Folder;
    }

    public FolderFile(FolderFile fatherFile, string fileName, int size, int startBlockIndex, string ownerAccountNumber, LimitType limitType)
            :base(fatherFile,fileName,size,startBlockIndex,ownerAccountNumber,limitType)
    {
        fileType = FileType.Folder;
    }

    public override void OpenFile()
    {
        FileNodeManager.Instance.currentFolder = this;
    }
}

[System.Serializable]
public class OtherFile:BaseFile
{
   public string content;            //文件内容

   public OtherFile(FolderFile fatherFile, string fileName, int size, int startBlockIndex, string ownerAccountNumber, LimitType limitType) 
        :base(fatherFile,fileName,size,startBlockIndex,ownerAccountNumber,limitType)
    {
        fileType = FileType.Other;
        content = "";
    }

    public override void OpenFile()
    {
        FileNodeManager.Instance.currentOtherFile = this;
    }
}


public enum FileType
{
    Folder,
    Other
}

public enum LimitType
{
    Free,  //没有任何限制
    Middle,   //只准读
    High      //禁止任何操作
}


