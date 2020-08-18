using System.Collections;
using System.Collections.Generic;
using UI_scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsLayout : MonoBehaviour
{

    public void OpenChangeDescriptionLayout()
    {
        Links.MainMenuController.changeDescriptionLayout.ShowInfo(Links.DeviceInformation.PlayerData);
    }

    public void LogOut()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
