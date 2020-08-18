using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    public Text changeThisText;
    public PlayerData playerData;
    
    public string Text
    {
        get { return changeThisText.text; }
        set { changeThisText.text = value; }
    }

    public void ButtonClick()
    {
        Links.FindUserLayout.ChoosePlayer(playerData);
    }
}