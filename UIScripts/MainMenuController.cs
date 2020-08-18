using System.Collections;
using System.Collections.Generic;
using ClientServerScripts;
using FantomLib;
using UI_scripts;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenuLayout;
    public GameObject WarmapLayout;
    public GameObject EventsLayout;
    public GameObject SettingsLayout;
    public GameObject TopPlayersLayout;
    public GameObject NotificationsLayout;
    public GameObject EventsInfoLayout;
    public GameObject CreateEventLayout;
    public GameObject Background;
    public GameObject ShopLayout;
    public GameObject CommentsLayout;
    public GameObject Managers;
    public GameObject Layouts;
    public GameObject Level;
    
    public FindUserLayout FindUserLayout;
    public RegisterEventLayout RegisterEventLayout;
    public EventsLayout EventsLayoutScript;
    public TopPlayersLayout TopPlayersLayoutScript;
    public PlayerInfoLayout PlayerInfoLayout;
    public CharacterSkin charSkin;
    public ChangeCharacterInfoLayout changeDescriptionLayout;
    public QRlayout QRlayout;
    public QRtask QRtask;
    
    void Start()
    {
//        DontDestroyOnLoad(Managers);
//        DontDestroyOnLoad(Layouts);
//        DontDestroyOnLoad(Managers);
        Links.MainMenuLayout = MainMenuLayout.GetComponent<MainMenuLayout>();
        Links.EventsLayout = EventsLayoutScript;
        Links.MainMenuController = this;
        Links.EventInfoLayout = EventsInfoLayout.GetComponent<EventInfoLayout>();
        Links.FindUserLayout = FindUserLayout;
        Links.RegisterEventLayout = RegisterEventLayout;
        Links.TopPlayersLayout = TopPlayersLayoutScript;
        TimePickerController.OnTimePicked += Links.RegisterEventLayout.OnTimeSelectEvent;
        DatePickerController.OnDatePicked += Links.RegisterEventLayout.OnDateSelectEvent;
        Links.PlayerInfoLayout = PlayerInfoLayout;
        CharacterSkin.Char = charSkin;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void ShowMainMenuLayout()
    {
        MainMenuLayout.SetActive(true);
    }

    public void HideMainMenuLayout()
    {
        MainMenuLayout.SetActive(false);
    }

    public void ShowWarmapLayout()
    {
        WarmapLayout.SetActive(true);
    }

    public void HideWarmapLayout()
    {
        WarmapLayout.SetActive(false);
    }

    public void ShowEventsLayout()
    {
        EventsLayout.SetActive(true);
    }

    public void HideEventsLayout()
    {
        EventsLayout.SetActive(false);
    }

    public void ShowSettingsLayout()
    {
        SettingsLayout.SetActive(true);
    }

    public void HideSettingsLayout()
    {
        SettingsLayout.SetActive(false);
    }


    public void ShowTopPlayersLayout()
    {
        TopPlayersLayout.SetActive(true);
    }

    public void HideTopPlayersLayout()
    {
        TopPlayersLayout.SetActive(false);
    }

    public void ShowNotificationsLayout()
    {
        NotificationsLayout.SetActive(true);
    }


    public void HideNotificationsLayout()
    {
        NotificationsLayout.SetActive(false);
    }
}