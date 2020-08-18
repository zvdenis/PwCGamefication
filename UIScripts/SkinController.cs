using System.Collections;
using System.Collections.Generic;
using FantomLib;
using GameLibrary;
using UI_scripts;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    public static SkinController skinController;

    public List<GameObject> Bodys;
    public List<SpriteRenderer> Weapons;
    public List<int> WeaponID;
    public List<SpriteRenderer> Heads;
    public List<int> HeadID;

    public List<ItemCollector> items = new List<ItemCollector>();

    private void Awake()
    {
        skinController = this;
    }

    private void Start()
    {
        skinController = this;
        foreach (var t in Bodys)
        {
            items.Add(new BodySpriteCollector(t.GetComponent<BodyCollector>()));
        }

        for (int i = 0; i < Weapons.Count; i++)
        {
            int id = WeaponID[i];
            SingleSpriteCollector single = new SingleSpriteCollector
            {
                id = id, type = ItemType.Weapon, sprite = Weapons[i]
            };
            items.Add(single);
        }


        for (int i = 0; i < Heads.Count; i++)
        {
            int id = HeadID[i];
            SingleSpriteCollector single = new SingleSpriteCollector
            {
                id = id, sprite = Heads[i], type = ItemType.Head
            };
            items.Add(single);
        }
    }

    public ItemCollector getByID(int id)
    {
        foreach (var t in items)
        {
            if (t.id == id)
                return t;
        }

        return null;
    }


    public Item GetItemByID(int id)
    {
        foreach (var item in ItemList.Items)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }
}