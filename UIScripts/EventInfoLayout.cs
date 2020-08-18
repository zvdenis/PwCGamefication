using System;
using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class EventInfoLayout : MonoBehaviour
{
    public Text Title;
    public Text Date;
    public Text Description;
    public GameObject AdminInfo;
    public GameObject AdminInfoParent;
    public GameObject ManagerInfoParent;
    public GameObject CommentsButton;
    public GameObject QRButton;

    public EventData Data;

    private void Start()
    {
        //Links.EventInfoLayout = this;

//        List<PlayerData> admins = new List<PlayerData>();
//        PlayerData a = new PlayerData(0,0,"Den", 0,0,0, "");
//        PlayerData b = new PlayerData(0,0,"DenMuden", 0,0,0, "");
//        PlayerData c = new PlayerData(0,0,"MudenDenMuden", 0,0,0, "");
//        admins.Add(a);
//        admins.Add(b);
//        admins.Add(c);
//        EventData data = new EventData(0,DateTime.Now, DateTime.Now, "PUTLER", "Putl\n\n\nlerler Пятаков РОман Ивванович по совместительству Слох самый лучший  тимлид и командир взвода!", 0, 0, admins, admins);
//        ShowEventInfo(data);
    }

    public void ShowEventInfo(EventData data)
    {
        Data = data;
        Title.text = data.title;
        string date = "";
        DateTime dateTime = data.start;
        date += dateTime.Day + "." + dateTime.Month + "." + dateTime.Year;
        date += "   " + dateTime.Hour + ":" + dateTime.Minute;
        Date.text = date;
        Description.text = data.description;

        foreach (var admin in data.admins)
        {
            GameObject tmp = Instantiate(AdminInfo);
            tmp.transform.SetParent(AdminInfoParent.transform);
            adminElementController adminElementController = tmp.GetComponent<adminElementController>();
            adminElementController.Text = admin.NickName;
            tmp.transform.localScale = Vector3.one;
        }

        foreach (var manager in data.managers)
        {
            GameObject tmp = Instantiate(AdminInfo);
            tmp.transform.SetParent(ManagerInfoParent.transform);
            adminElementController adminElementController = tmp.GetComponent<adminElementController>();
            adminElementController.Text = manager.NickName;
            tmp.transform.localScale = Vector3.one;
        }

        gameObject.SetActive(true);
    }

    public void ShowComments()
    {
        //TODO
        CommentsLayout.EventID = Data.id;
        Links.RequestController.RequsetComments(Data.id);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void QRButtonPressed()
    {
        Links.RequestController.RequestQRhash(Data.id);
        Links.MainMenuController.QRlayout.gameObject.SetActive(true);
    }
}