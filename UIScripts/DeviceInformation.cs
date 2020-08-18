using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UI_scripts;
using UnityEngine;

public class DeviceInformation : MonoBehaviour
{
    private PlayerData playerData;

    public PlayerData PlayerData
    {
        get { return playerData; }
        set
        {
            playerData = value;
            UpdatePlayer();
        }
    }


    private List<EventData> futureEvents;
    public List<EventUserData> PlayerEvents;

    public List<EventData> FutureEvents
    {
        get { return futureEvents; }
        set
        {
            futureEvents = value;
            Links.EventsLayout.UpdateList(ToEventUserDatas(value));
        }
    }

    public List<EventUserData> ToEventUserDatas(List<EventData> datas)
    {
        List<EventUserData> newData = new List<EventUserData>();
        foreach (var data in datas)
        {
            EventUserData userData = new EventUserData(data.id, data.start, data.end, data.title, data.description,
                data.ratingAward, data.moneyAward, data.admins, data.managers, isParticipating(data));
        }

        return newData;
    }

    //Saved Data
    public bool BanNotification
    {
        get { return PlayerPrefs.GetInt("BanNotification") == 1; }
        set { PlayerPrefs.SetInt("BanNotification", value ? 1 : 0); }
    }

    private void Awake()
    {
        Links.DeviceInformation = this;
    }

    private void Start()
    {
    }

    public bool isParticipating(EventData eventData)
    {
        return isParticipating(eventData.id);
    }

    public bool isParticipating(int eventId)
    {
        foreach (var playerEvent in PlayerEvents)
        {
            if (playerEvent.id == eventId && playerEvent.visited)
                return true;
        }

        return false;
    }

    void UpdatePlayer()
    {
        Links.MainMenuLayout.scoreText.text = playerData.Money.ToString();
        Links.MainMenuLayout.ratingText.text = playerData.Rating.ToString();
        CharacterSkin.Char.PutItems(playerData.WornItems);
    }

    void UpdateReview(List<ReviewData> datas)
    {
        foreach (var eventUser in PlayerEvents)
        {
        }
    }
}