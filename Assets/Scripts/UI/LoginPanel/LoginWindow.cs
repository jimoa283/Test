using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginWindow : MonoBehaviour
{
    private InputField accountNumberField;
    private InputField passwordField;
    private Text accountNumberErrorText;
    private Text passwordErrorText;
    private Button loginButton;
    private Button registerButtton;
    private Button exitButton;

    [SerializeField]private string accountNumberValue;
    [SerializeField]private string passwordValue;

    public void Init()
    {
        Transform accountNumber = transform.GetChildTransform("AccountNumber");
        accountNumberField = accountNumber.GetChildTransform("InputField").GetComponent<InputField>();
        accountNumberErrorText = accountNumber.GetChildTransform("ErrorText").GetComponent<Text>();

        Transform password = transform.GetChildTransform("Password");
        passwordField = password.GetChildTransform("InputField").GetComponent<InputField>();
        passwordErrorText = password.GetChildTransform("ErrorText").GetComponent<Text>();

        loginButton = transform.GetChildTransform("LoginButton").GetComponent<Button>();
        registerButtton = transform.GetChildTransform("RegisterButton").GetComponent<Button>();
        exitButton = transform.GetChildTransform("ExitButton").GetComponent<Button>();

        accountNumberErrorText.gameObject.SetActive(false);
        passwordErrorText.gameObject.SetActive(false);

        loginButton.onClick.AddListener(Login);
        registerButtton.onClick.AddListener(Register);

        accountNumberField.onValueChanged.AddListener(value =>accountNumberValue = value);
        passwordField.onValueChanged.AddListener(value =>passwordValue = value);
        exitButton.onClick.AddListener(Exit);

    }

    public void ResetWindow()
    {
        accountNumberErrorText.gameObject.SetActive(false);
        passwordErrorText.gameObject.SetActive(false);

        accountNumberField.text = "";
        accountNumberValue = "";
        passwordField.text = "";
        passwordValue = "";
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Login()
    {
        accountNumberErrorText.gameObject.SetActive(false);
        passwordErrorText.gameObject.SetActive(false);

        if (!UserDataManager.Instance.CheckUserExist(accountNumberValue))
        {
            accountNumberErrorText.gameObject.SetActive(true);
            return;
        }

        if(!UserDataManager.Instance.CheckUserPassWord(accountNumberValue,passwordValue))
        {
            passwordErrorText.gameObject.SetActive(true);
            return;
        }

        FileNodeManager.Instance.currentFolder = FileNodeManager.Instance.rootFolder;

        UserDataManager.Instance.SetCurrentUser(accountNumberValue);      

        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.MainPanel));
    }

    private void Register()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.RegisterPanel));
    }
}
