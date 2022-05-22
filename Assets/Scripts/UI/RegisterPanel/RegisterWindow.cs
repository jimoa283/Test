using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindow : MonoBehaviour
{
    private InputField accountNumberField;
    private InputField passwordField;
    private Text accountNumberErrorText;
    private Text passwordErrorText;
    private InputField passwordAgainField;
    private Text passwordAgainErrorText;
    private Button registerButtton;
    private Button returnButton;
    private Dropdown dropdown;

    [SerializeField]private UserType userType;
    [SerializeField]private string accountNumberValue;
    [SerializeField]private string passwordValue;
    [SerializeField] private string passwordAgainValue;


    public void Init()
    {
        Transform accountNumber = transform.GetChildTransform("AccountNumber");
        accountNumberField = accountNumber.GetChildTransform("InputField").GetComponent<InputField>();
        accountNumberErrorText = accountNumber.GetChildTransform("ErrorText").GetComponent<Text>();

        Transform password = transform.GetChildTransform("Password");
        passwordField = password.GetChildTransform("InputField").GetComponent<InputField>();
        passwordErrorText = password.GetChildTransform("ErrorText").GetComponent<Text>();

        Transform passwordAgain = transform.GetChildTransform("PasswordAgain");
        passwordAgainField = passwordAgain.GetChildTransform("InputField").GetComponent<InputField>();
        passwordAgainErrorText = passwordAgain.GetChildTransform("ErrorText").GetComponent<Text>();

        registerButtton = transform.GetChildTransform("RegisterButton").GetComponent<Button>();
        returnButton = transform.GetChildTransform("ReturnButton").GetComponent<Button>();
        dropdown = GetComponentInChildren<Dropdown>();

        accountNumberField.onValueChanged.AddListener(value =>accountNumberValue = value);
        passwordField.onValueChanged.AddListener(value =>passwordValue = value);
        passwordAgainField.onValueChanged.AddListener(value =>passwordAgainValue = value);

        accountNumberErrorText.gameObject.SetActive(false);
        passwordErrorText.gameObject.SetActive(false);
        passwordAgainErrorText.gameObject.SetActive(false);

        registerButtton.onClick.AddListener(Register);
        returnButton.onClick.AddListener(() => UIManager.Instance.PopPanel(UIPanelType.RegisterPanel));
        dropdown.onValueChanged.AddListener(x =>
        {
            switch (x)
            {
                case 0:
                    userType = UserType.Manager;
                    break;
                default:
                    userType = UserType.Common;
                    break;
            };
        });
        dropdown.value = 1;
    }

    public void ResetWindow()
    {
        accountNumberErrorText.gameObject.SetActive(false);
        passwordErrorText.gameObject.SetActive(false);
        passwordAgainErrorText.gameObject.SetActive(false);

        accountNumberField.text = "";
        accountNumberValue = "";

        passwordField.text = "";
        passwordValue = "";

        passwordAgainField.text = "";
        passwordAgainValue = "";

        dropdown.value = 1;
    }

    private void Register()
    {
        accountNumberErrorText.gameObject.SetActive(false);
        passwordErrorText.gameObject.SetActive(false);
        passwordAgainErrorText.gameObject.SetActive(false);

        if (UserDataManager.Instance.CheckUserExist(accountNumberValue))
        {
            accountNumberErrorText.gameObject.SetActive(true);
            return;
        }

        if(!UserDataManager.Instance.CheckPasserWordExist(passwordValue))
        {
            passwordErrorText.gameObject.SetActive(true);
            return;
        }

        if(passwordAgainValue!=passwordValue)
        {
            passwordAgainErrorText.gameObject.SetActive(true);
            return;
        }

        UserDataManager.Instance.CreateNewUser(accountNumberValue, passwordValue,userType);

        UIManager.Instance.SetSimpleWindow("注册成功", () => 
        {
            UIManager.Instance.PopPanel(UIPanelType.RegisterPanel);
            //UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.LoginPanel));
         }) ; 
    }

}
