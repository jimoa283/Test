using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserDataManager : Singleton<UserDataManager>
{
    private Dictionary<string,UserData> userDataDic;            //用户字典（Map）
    //private UserDataSave currentUserDataSave;    
    private UserData currentUser;                               //当前的用户
    private UserDataContainerSO userDataContainer;

    public UserData CurrentUser { get => currentUser;}

    public UserDataManager()
    {
        userDataDic = new Dictionary<string, UserData>();
        //currentUserDataSave = new UserDataSave();
        //if (!File.Exists(Application.dataPath + "/UserData/userData.json"))
        //return;

        /*StreamReader sr = new StreamReader(Application.dataPath + "/UserData/userData.json");
        string jsonString = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();

        currentUserDataSave = JsonUtility.FromJson<UserDataSave>(jsonString);

        for (int i = 0; i < currentUserDataSave.accountNumberList.Count; i++)
        {
            UserData userData = new UserData
            {
                AccountNumber = currentUserDataSave.accountNumberList[i],
                Password = currentUserDataSave.passwordList[i]
            };

            userDataDic.Add(userData.AccountNumber, userData);
        }*/

        userDataContainer = Resources.Load<UserDataContainerSO>("SO/UserDataContainer");
        foreach(var userData in userDataContainer.userDataList)
        {
            userDataDic.Add(userData.AccountNumber, userData);
        }
    }

    public void SetCurrentUser(string account)
    {
        currentUser = userDataDic[account];
        //currentUser=userDataContainer.userDataList.Find()
    }

    public bool CheckUserExist(string accountNumber)
    {
        return !(userDataDic.Count == 0 || !userDataDic.ContainsKey(accountNumber));
        //return !(userDataContainer.userDataDic.Count == 0 || !userDataContainer.userDataDic.ContainsKey(accountNumber));
    }

    public bool CheckUserPassWord(string account,string password)
    {
        return userDataDic[account].Password == password;
        //return userDataContainer.userDataDic[account].Password == password;
    }

    public void CreateNewUser(string account, string password, UserType userType)
    {
        UserData userData = new UserData()
        {
            AccountNumber = account,
            Password = password,
            UserType=userType
        };

        userDataContainer.userDataList.Add(userData);
        userDataDic.Add(account, userData);
       /* userDataDic.Add(account, userData);
        currentUserDataSave.AddNewUser(userData);

        if (!Directory.Exists(Application.dataPath + "/UserData"))
            Directory.CreateDirectory(Application.dataPath + "/UserData");

        StreamWriter sw = new StreamWriter(Application.dataPath + "/UserData/userData.json");
        string json = JsonUtility.ToJson(currentUserDataSave);

        sw.WriteLine(json);
        sw.Close();
        sw.Dispose();*/
    }

    public bool CheckPasserWordExist(string password)
    {
        foreach(var userData in userDataDic)
        {
            if (userData.Value.Password == password)
                return false;
        }

        return true;
    }
}

public class UserDataSave
{
    public List<string> accountNumberList = new List<string>();
    public List<string> passwordList = new List<string>();

    public void AddNewUser(UserData userData)
    {
        accountNumberList.Add(userData.AccountNumber);
        passwordList.Add(userData.Password);
    }
}

[System.Serializable]
public class UserData
{
    public string AccountNumber;            //账号
    public string Password;                 //密码
    public UserType UserType;              //用户类型(管理员，一般用户）
}

public enum UserType
{
    Manager,
    Common
}
