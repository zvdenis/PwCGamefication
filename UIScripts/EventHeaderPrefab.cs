using System.Collections;
using System.Collections.Generic;
using FantomLib;
using GameLibrary;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class EventHeaderPrefab : MonoBehaviour
{
    public Text TitleText;
    public Text TimeText;
    public EventData EventData;
    public EventParticipateCheckMark Mark;
    
    
    public string Title
    {
        get { return TitleText.text; }
        set { TitleText.text = value; }
    }

    public string Time
    {
        get { return TimeText.text; }
        set { TimeText.text = value; }
    }

    public void ShowDetails()
    {
        Links.EventInfoLayout.ShowEventInfo(EventData);
    }
}