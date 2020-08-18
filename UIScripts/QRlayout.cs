using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using GameLibrary;
using UI_scripts;
using UnityEngine;
using UnityEngine.UI;

public class QRlayout : MonoBehaviour
{
    public Image QRimage;
    private Texture2D texture2D;

    public void ShowLayout(String data)
    {
        gameObject.SetActive(true);
        Links.MainMenuController.QRtask.QrCodeEncodeController.Encode(data);
    }

    public void Draw(Texture2D texture)
    {
        texture2D = texture;
        QRimage.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
            new Vector2(0.5f, 0.5f), 100.0f);
        gameObject.SetActive(true);
    }

    public void SaveButtonPressed()
    {
        GalleryController.SaveImageToGallery(texture2D);
    }
}