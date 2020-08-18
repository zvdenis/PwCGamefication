using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ClientServerScripts;
using UI_scripts;
using UnityEngine;

public class LoginLayoutsController : MonoBehaviour
{
    public GameObject LoginLayout;
    public GameObject LoadingLayout;
    public GameObject SignupLayout;

    private void Start()
    {
        Links.LoginLayoutController = this;
    }

    public void LogInPressed()
    { 
        ShowLoadingLayout();
        HideLoginLayout();
        SendEnteredFields();
    }

    public void SendEnteredFields()
    {
        ShowLoadingLayout();
        HideLoginLayout();
        Links.RequestController.TryLogIn(Links.LoginLayout.Login, Links.LoginLayout.Password);
    }


    public void CreateAccountPressed()
    {
        string login = Links.SignupLayout.Login;
        string password = Links.SignupLayout.Password;
        string confirmPassword = Links.SignupLayout.ConfirmedPassword;
        string message = "";

        if (!FirstCheck(login, password, confirmPassword, ref message))
        {
            Links.SignupLayout.TextForMessages.text = message;
            return;
        }

        ShowLoadingLayout();
        HideLoginLayout();

        RegisterNewUser();
    }

    bool FirstCheck(string login, string password, string confirmPassword, ref string message)
    {
        if (password != confirmPassword)
        {
            message = "passwords do not match";
            return false;
        }

        if (password.Length < 3)
        {
            message = "password should have at least 3 characters";
            return false;
        }

        if (login.Length < 5)
        {
            message = "login should have at least 5 characters";
            return false;
        }
        
        if (password.Length > 20)
        {
            message = "password should have at most 20 characters";
            return false;
        }

        if (login.Length > 29)
        {
            message = "login should have at most 30 characters";
            return false;
        }
        

        if (!IsValidEmail(login))
        {
            message = "invalid Email";
            return false;
        }
        
        return true;
    }
    
    bool IsValidEmail(string email)
    {
        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch {
            return false;
        }
    }

    public void RegisterNewUser()
    {
        ShowLoadingLayout();
        HideSignupLayout();
        RequestResult result = new RequestResult();
        Links.RequestController.TryRegister(Links.SignupLayout.Login, Links.SignupLayout.Password, Links.SignupLayout.Nickname);
    }


    public void GoToSignUpButtonPressed()
    {
        ShowSignupLayout();
        HideLoginLayout();
    }


    public void HideLoginLayout()
    {
        LoginLayout.SetActive(false);
    }

    public void ShowLoginLayout()
    {
        LoginLayout.SetActive(true);
    }

    public void HideLoadingLayout()
    {
        LoadingLayout.SetActive(false);
    }

    public void ShowLoadingLayout()
    {
        LoadingLayout.SetActive(true);
    }


    public void HideSignupLayout()
    {
        SignupLayout.SetActive(false);
    }

    public void ShowSignupLayout()
    {
        SignupLayout.SetActive(true);
    }

    public void OpenMainMenu()
    {
        Links.MainMenuController.ShowMainMenuLayout();
        HideLoginLayout();
        HideSignupLayout();
        HideLoadingLayout();
    }

    public void OpenLoginLayoutWithError(string error)
    {
        Links.LoginLayout.TextForMessages.text = error;
        OpenLoginLayout();
    }

    public void OpenRegisterWithError(string error)
    {
        Links.SignupLayout.TextForMessages.text = error;
        ShowSignupLayout();
        HideLoadingLayout();
        HideLoginLayout();
    }

    public void OpenLoginLayout()
    {
        ShowLoginLayout();
        HideSignupLayout();
        HideLoadingLayout();
    }
}