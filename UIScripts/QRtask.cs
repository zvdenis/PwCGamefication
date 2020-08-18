using System.Collections;
using System.Collections.Generic;
using UI_scripts;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QRtask : MonoBehaviour
{
    public QRCodeDecodeController QrCodeDecodeController;
    public QRCodeEncodeController QrCodeEncodeController;
    public GameObject QRobject;
    public GameObject mainCamera;
    public GameObject alterCamera;

    // Start is called before the first frame update
    void Start()
    {
        Links.QRtask = this;
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif
        QrCodeDecodeController.onQRScanFinished += getResult;
        QrCodeEncodeController.onQREncodeFinished += Links.MainMenuController.QRlayout.Draw;
    }

    void getResult(string resultStr)
    {
        Debug.Log(resultStr);
        Links.RequestController.RequestVisit(resultStr);
        BackButton();
    }

    private void OnDisable()
    {
        QrCodeDecodeController.StopWork(); 
    }

    public void BackButton()
    {
        QRtask task = Links.QRtask;
        task.mainCamera.SetActive(true);
        task.alterCamera.SetActive(false);
        task.QRobject.SetActive(false);
        task.QrCodeDecodeController.StopWork();
    }
    
}