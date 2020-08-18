using System.Collections;
using System.Collections.Generic;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class SignupLayout : MonoBehaviour
{
    public InputField PasswordField;
    public InputField ConfirmPasswordField;
    public InputField MailField;
    public InputField NicknameField;
    public Text TextForMessages;

    public string Login
    {
        get { return MailField.text; }
    }

    public string Password
    {
        get { return PasswordField.text; }
    }

    public string ConfirmedPassword
    {
        get { return ConfirmPasswordField.text; }
    }

    public string Nickname
    {
        get { return NicknameField.text; }
    }

    private void Start()
    {
        Links.SignupLayout = this;
    }
}