using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="User/UserDataContainer")]
[System.Serializable]
public class UserDataContainerSO : ScriptableObject
{
    public List<UserData> userDataList = new List<UserData>();
}
