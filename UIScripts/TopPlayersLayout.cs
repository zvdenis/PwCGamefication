using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UI_scripts;
using UnityEngine;

public class TopPlayersLayout : MonoBehaviour
{
    public GameObject playerElement;
    public GameObject playerElementParent;
    public List<PlayerData> PlayerDatas;
    public List<GameObject> PlayerObjects;

    private void Start()
    {
//        List<PlayerData> admins = new List<PlayerData>();
//        PlayerData a = new PlayerData(0, 0, "Den", 0, 0, 0, "");
//        PlayerData b = new PlayerData(0, 0, "DenMuden", 0, 0, 0, "");
//        PlayerData c = new PlayerData(0, 0, "MudenDenMuden", 0, 0, 0, "");
//        admins.Add(a);
//        admins.Add(b);
//        admins.Add(c);
//        UpdateTop(admins);
    }

    private void OnEnable()
    {
        Links.RequestController.RequestLeaderboard();
    }

    public void UpdateTop(List<PlayerData> playerDatas)
    {
        foreach (var playerObject in PlayerObjects)
        {
            Destroy(playerObject);
        }

        PlayerDatas = playerDatas;
        PlayerData data;
        for (int i = 0; i < playerDatas.Count; i++)
        {
            data = playerDatas[i];
            GameObject tmp = Instantiate(playerElement, playerElementParent.transform);
            PlayerObjects.Add(tmp);
            LeaderBoardElement leaderBoardElement = tmp.GetComponent<LeaderBoardElement>();
            leaderBoardElement.PlayerData = data;
            leaderBoardElement.playerPosition.text = "#" + (i + 1);
        }
    }
}