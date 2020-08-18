using System.Collections;
using System.Collections.Generic;
using FantomLib;
using GameLibrary;
using UI_scripts; 
using UnityEngine;
using UnityEngine.UI; 
public class ConfirmPanel : MonoBehaviour
{
    public Text PriceText;
    public Image ItemImage;
    private int ItemID;

    public void ShowPanel(int id)
    {
        ItemID = id;
        ItemImage.sprite = ShopController.shop.GetSpriteById(id);
        Item item = SkinController.skinController.GetItemByID(id);
        PriceText.text = item.ItemPower.Price.ToString();
        
    }
    
    
    public void OkClicked()
    {
        if (SkinController.skinController.GetItemByID(ItemID).ItemPower.Price <=
            Links.DeviceInformation.PlayerData.Money)
        {
            gameObject.SetActive(false);
            Links.RequestController.RequestItemBuy(ItemID);
            Links.ToastController.Show("Запрос на покупку отправлен");
        }
        else
        {
            gameObject.SetActive(false);
            Links.ToastController.Show("Недостаточно монет!");
        }
    }

    public void CancelClicked()
    {
        gameObject.SetActive(false);
    }
}
