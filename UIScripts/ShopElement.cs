using System;
using System.Collections;
using System.Collections.Generic;
using FantomLib;
using GameLibrary;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    public Image ElementImage;
    public GameObject WearImage;
    public Text AttackText;
    public Text HealthText;
    public Text PriceText;
    public GameObject PriceTextObject;

    public Power ItemPower;
    public int ItemID;

    public void SetItem(int id)
    {
        long wornItems = Links.DeviceInformation.PlayerData.WornItems;
        List<long> wornItemsID = ItemList.GetIds(wornItems);

        long hasItems = Links.DeviceInformation.PlayerData.Items;
        List<long> hasItemsID = ItemList.GetIds(hasItems);

        ItemID = id;
        List<Item> items = ItemList.Items;
        Item curItem = ItemList.GetItemByID(id);
        ItemPower = curItem.ItemPower;

        if (hasItemsID.Contains(id))
        {
            //Предмет куплен
            PriceTextObject.SetActive(false);
            if (wornItemsID.Contains(id))
            {
                //предмет надет
                WearImage.SetActive(true);
            }
        }

        AttackText.text = ItemPower.Attack.ToString();
        HealthText.text = ItemPower.Health.ToString();
        PriceText.text = ItemPower.Price.ToString();
        ElementImage.sprite = ShopController.shop.GetSpriteById(id);
    }

    public void ItemClick()
    {
        long wornItems = Links.DeviceInformation.PlayerData.Items;
        List<long> wornItemsID = ItemList.GetIds(wornItems);

        long hasItems = Links.DeviceInformation.PlayerData.Items;
        List<long> hasItemsID = ItemList.GetIds(wornItems);

        if (hasItemsID.Contains(ItemID))
        {
            Links.RequestController.RequestPutItem(ItemID);
            //Предмет куплен
            long newCode = ItemList.PutItem(ItemID, Links.DeviceInformation.PlayerData.WornItems);
            //Links.DeviceInformation.PlayerData.WornItems = newCode;
            PlayerData oldPlayerData = Links.DeviceInformation.PlayerData;
            PlayerData newPlayerData = new PlayerData(oldPlayerData.Items, newCode, oldPlayerData.NickName,
                oldPlayerData.Rating, oldPlayerData.Money, oldPlayerData.Id, oldPlayerData.Email,
                oldPlayerData.castleId, oldPlayerData.Description, oldPlayerData.isBanned, DateTime.Now);
            Links.DeviceInformation.PlayerData = newPlayerData;
            Links.MainMenuController.ShopLayout.GetComponent<ShopLayout>().UpdateShop();
        }
        else
        {
            //предмет не куплен
            ShopLayout shop = Links.MainMenuController.ShopLayout.GetComponent<ShopLayout>();
            shop.panel.SetActive(true);
            shop.panel.GetComponent<ConfirmPanel>().ShowPanel(ItemID);
        }


        
    }
}