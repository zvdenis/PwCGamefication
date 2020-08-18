using System.Collections;
using System.Collections.Generic;
using System.Threading;
using GameLibrary;
using UI_scripts;
using UnityEngine;

public class CharacterSkin : MonoBehaviour
{
    public static CharacterSkin Char;
    private float timer = 0.1f;

    public GameObject MainBone;

    public SpriteRenderer Body;
    public SpriteRenderer Weapon;
    public SpriteRenderer Back;
    public SpriteRenderer Head;

    public SpriteRenderer LeftEye;
    public SpriteRenderer LeftHand;
    public SpriteRenderer LeftLeg;

    public SpriteRenderer RightEye;
    public SpriteRenderer RightHand;
    public SpriteRenderer RightLeg;


    public void PutItem(int id)
    {
        ItemCollector item = SkinController.skinController.getByID(id);
        switch (item.type)
        {
            case ItemType.Body:
                BodySpriteCollector body = (BodySpriteCollector) item;
                if (body.back == null)
                    Back.sprite = null;
                else
                    Back.sprite = body.back.sprite;
                Body.sprite = body.body.sprite;
                LeftEye.sprite = body.leftEye.sprite;
                RightEye.sprite = body.rightEye.sprite;
                LeftHand.sprite = body.leftArm.sprite;
                RightHand.sprite = body.rightArm.sprite;
                LeftLeg.sprite = body.leftLeg.sprite;
                RightLeg.sprite = body.rightLeg.sprite;
                break;
            case ItemType.Head:

                SingleSpriteCollector head = (SingleSpriteCollector) item;
                Head.sprite = head.sprite.sprite;
                break;
            case ItemType.Weapon:
                SingleSpriteCollector weapon = (SingleSpriteCollector) item;
                Weapon.sprite = weapon.sprite.sprite;
                break;
        }
    }

    public void PutItems(long code)
    {
        List<long> ids = ItemList.GetIds(code);
        foreach (var id in ids)
        {
            PutItem((int)id);
        }
    }
 

    private void UpdateSkin()
    {
    }

    public void Animate()
    {
        GetComponent<Animator>().Play("New Animation");
    }

    IEnumerator scaleBody(Vector3 was)
    {
        yield return new WaitForSeconds(timer);
        MainBone.transform.localScale = was;
    }

    private void Start()
    { 
    }
}