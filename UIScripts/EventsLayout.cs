using System;
using System.Collections.Generic;
using GameLibrary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI_scripts
{
    public class EventsLayout : MonoBehaviour
    {
        public GameObject EventElement;
        public GameObject EventElementParent;

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<EventUserData> datas;

        // Start is called before the first frame update
        void Start()
        {
//            List<PlayerData> admins = new List<PlayerData>();
//            PlayerData a = new PlayerData(0, 0, "Den", 0, 0, 0, "");
//            PlayerData b = new PlayerData(0, 0, "DenMuden", 0, 0, 0, "");
//            PlayerData c = new PlayerData(0, 0, "MudenDenMuden", 0, 0, 0, "");
//            admins.Add(a);
//            admins.Add(b);
//            admins.Add(c);
//            EventData data = new EventData(0, DateTime.Now, DateTime.Now, "PUTLER",
//                "Putl\n\n\nlerler Пятаков РОман Ивванович по совместительству Слох самый лучший  тимлид и командир взвода!",
//                0, 0, admins, admins);
//
//            List<EventData> events = new List<EventData>();
//            events.Add(data);
//            Links.DeviceInformation.FutureEvents = events;
        }

        private void OnEnable()
        {
            Links.RequestController.RequestFutureEvents();
            Links.RequestController.RequestPlayerUpdate();
        }

        public void ShowCreationLayout()
        {
            Links.RegisterEventLayout.Show();
        }

        public void UpdateList(List<EventUserData> list)
        {
            datas = list;
            foreach (var gameObject in gameObjects)
            {
                Destroy(gameObject);
            }

            gameObjects.Clear();

            foreach (var data in datas)
            {
                GameObject tmp = Instantiate(EventElement, EventElementParent.transform);
                gameObjects.Add(tmp);
                EventHeaderPrefab eventHeaderPrefab = tmp.GetComponent<EventHeaderPrefab>();

                if (data.visited)
                {
                    eventHeaderPrefab.Mark.ChangeCheckMark();
                }

                eventHeaderPrefab.EventData = data;
                eventHeaderPrefab.Title = data.title;
                eventHeaderPrefab.Time = data.start.Day + "." + data.start.Month + "  " +
                                         data.start.Hour + ":" + data.start.Minute;
            }
        }

        public void CameraButtonClicked()
        {
            QRtask task = Links.QRtask;
            task.mainCamera.SetActive(false);
            task.alterCamera.SetActive(true);
            task.QRobject.SetActive(true);
            task.QrCodeDecodeController.StartWork();
        }
    }
}