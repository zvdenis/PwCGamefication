using System.Collections;
using System.Collections.Generic;
using UI_scripts;
using UnityEngine;

public class EventParticipateCheckMark : MonoBehaviour
{
    public GameObject CheckMark;
    public EventHeaderPrefab HeaderPrefab;
    public string FalseMessage;
    public string TrueMessage;
    public bool participating = false;

    public void ChangeCheckMark()
    {
        participating = !participating;
        CheckMark.SetActive(participating);
    }

    public void Send()
    {
        Links.ToastController.Show(participating ? TrueMessage : FalseMessage);
        if (participating)
        {
            Links.RequestController.RequestEventSubscription(HeaderPrefab.EventData);
        }
        else
        {
            Links.RequestController.RequestEventUnsubscription(HeaderPrefab.EventData);
        }
    }
}