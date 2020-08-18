using System;
using System.Collections;
using System.Collections.Generic;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Slider>().value = Links.DeviceInformation.BanNotification ? 0 : 1;
    }

    public void SetBanNotification(Single value)
    {
        Links.DeviceInformation.BanNotification = value < 0.5f;
    }
    
}