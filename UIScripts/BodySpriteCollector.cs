using System.Collections;
using System.Collections.Generic;
using GameLibrary;
using UI_scripts;
using UnityEngine;

public class BodySpriteCollector : ItemCollector
{
    public SpriteRenderer body;
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    public SpriteRenderer leftLeg;
    public SpriteRenderer rightLeg;
    public SpriteRenderer leftEye;
    public SpriteRenderer rightEye;
    public SpriteRenderer back;

    public BodySpriteCollector(BodyCollector bodyCollector)
    {
        body = bodyCollector.body;
        leftArm = bodyCollector.leftArm;
        rightArm = bodyCollector.rightArm;
        leftLeg = bodyCollector.leftLeg;
        rightLeg = bodyCollector.rightLeg;
        leftEye = bodyCollector.leftEye;
        rightEye = bodyCollector.rightEye;
        back = bodyCollector.back;
        id = bodyCollector.id;
        type = ItemType.Body;
    }
}