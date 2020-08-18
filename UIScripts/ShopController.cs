using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopController : MonoBehaviour
{
    public List<Sprite> sprites;
    public List<int> ids;

    public static ShopController shop;

    private void Awake()
    {
        shop = this;
    }

    void Start()
    {
        shop = this;
    }

    public Sprite GetSpriteById(int id)
    {
        for (var i = 0; i < ids.Count; i++)
        {
            if (id == ids[i])
                return sprites[i];
        }

        for (var i = 0; i < SkinController.skinController.HeadID.Count; i++)
        {
            if (id == SkinController.skinController.HeadID[i])
                return SkinController.skinController.Heads[i].sprite;
        }

        for (var i = 0; i < SkinController.skinController.WeaponID.Count; i++)
        {
            if (id == SkinController.skinController.WeaponID[i])
                return SkinController.skinController.Weapons[i].sprite;
        }

        return null;
    }
}