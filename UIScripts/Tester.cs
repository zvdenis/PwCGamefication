using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using GameLibrary;
using UI_scripts;
using Unity.Collections;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delay());
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(3f);
        PlayerData data = new PlayerData(65535, 14, "zvdenis", 100, 20, 1, "zvdenis@mail.ru", -1, "i'm funny boy",
            false, DateTime.Now);
        List<PlayerData> playerDatas = new List<PlayerData>();
        playerDatas.Add(data);
        playerDatas.Add(data);

        DataInfo dataInfo = new ResponseUser(ResponseUser.ResponseUserType.Authorization, data);


        EventData eventData = new EventData(0, DateTime.Now, DateTime.Now, "футбеч", "кул футбеч)0", 1, 1, playerDatas,
            playerDatas);
        List<EventData> eventDatas = new List<EventData>();
        eventDatas.Add(eventData);
        ResponseEvent responseEvent = new ResponseEvent(eventDatas);
        Links.RequestController.ResponseInfoReceieved(responseEvent);

        Links.RequestController.ResponseInfoReceieved(dataInfo);
//        Links.MainMenuController.PlayerInfoLayout.ShowInfo(data);
//        ReviewData rd = new ReviewData(0, "Den4iK", "))))\n\n\n )) ) ) sd d )", DateTime.Now);
//        ReviewData rd2 = new ReviewData(0, "Max_FURER", "123456 1 2 3 4 5 6 server date ddd  s ss date tiem  n o ", DateTime.Now);
//        List<ReviewData> reviewDatas = new List<ReviewData>();
//        reviewDatas.Add(rd);
//        reviewDatas.Add(rd2);
//        Links.MainMenuController.CommentsLayout.GetComponent<CommentsLayout>().ShowComments(reviewDatas);
    }
}