using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoLayout : MonoBehaviour
{
    public Text PlayerNicknameText;
    public Text PlayerRatingText;
    public Text PlayerMailText;
    public Text DescriptionText;
    public CharacterSkin Character;

    public void ShowInfo(PlayerData playerData)
    {
        PlayerNicknameText.text = playerData.NickName;
        PlayerRatingText.text = playerData.Rating.ToString();
        PlayerMailText.text = playerData.Email;
        DescriptionText.text = playerData.Description;
        Character.PutItems(playerData.WornItems);
        gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        
    }
}