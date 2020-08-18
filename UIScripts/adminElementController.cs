using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class adminElementController : MonoBehaviour
{
    public Text text;
    public PlayerWrapper PlayerWrapper;

    public Text TextComponent
    {
        get
        {
            if (text == null)
                text = GetComponent<Text>();
            return text;
        }
    }

    public string Text
    {
        get { return TextComponent.text; }
        set { TextComponent.text = value; }
    }

    public void CancelButtonPressed()
    {        
        Links.RegisterEventLayout.DeletePlayer(PlayerWrapper);
    }
}