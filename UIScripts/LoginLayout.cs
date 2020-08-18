using System;
using System.Collections;
using System.Collections.Generic;
using UI_scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LoginLayout : MonoBehaviour
{
    public InputField PasswordField;
    public InputField LoginField;
    public Text TextForMessages;

    public string Login
    {
        get { return LoginField.text; }
    }

    public string Password
    {
        get { return PasswordField.text; }
    }

    private void Start()
    {
        Links.LoginLayout = this;
        StartCoroutine(Delay());
    }

    public void OpenApp()
    {
        Links.RequestController.EnterAppRequest();
    }

    private IEnumerator Delay()
    {
        yield return  new WaitForSeconds(1);
        OpenApp();
    }
}