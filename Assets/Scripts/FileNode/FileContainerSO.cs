using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="File/FileContainer")]
[System.Serializable]
public class FileContainerSO : ScriptableObject
{
    public FolderFile rootFile;
    public List<BaseFile> fileList = new List<BaseFile>();
}
