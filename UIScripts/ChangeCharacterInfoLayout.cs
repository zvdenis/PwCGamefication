using System;
using System.Collections;
using System.Collections.Generic;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacterInfoLayout : PlayerInfoLayout
{
    public InputField Description;
    private void OnEnable()
    {
        Description.text = Links.DeviceInformation.PlayerData.Description;
    }

    public void ConfirmChanges()
    {
        String newDescription = DescriptionText.text;
        Links.RequestController.RequestDescriptionChange(newDescription);
        Links.ToastController.Show("Запрос отправлен");
    }
}