using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardElement : MonoBehaviour
{
    public Text playerName;
    public Text playerScore;
    public Text playerPosition;

    private PlayerData _playerData;
    public PlayerData PlayerData
    {
        get { return _playerData; }
        set
        {
            _playerData = value;
            playerName.text = _playerData.NickName;
            playerScore.text = _playerData.Rating.ToString();
        }
    }

    
    
    public void ShowPlayerInfo()
    {
        
    }
}
