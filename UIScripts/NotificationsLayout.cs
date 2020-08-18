using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace UI_scripts
{
    public class NotificationsLayout : MonoBehaviour
    {
        class NotificationInfo
        {
            internal String title;
            internal String description;
        }

        public GameObject NotificationElement;
        public GameObject NotificationElementParent;

        private List<NotificationInfo> notificationInfos;
        private List<GameObject> notificationObjects = new List<GameObject>();

        private void OnEnable()
        {
            UpdateNotifications();
        }

        public void UpdateNotifications()
        {
            foreach (var obj in notificationObjects)
            {
                Destroy(obj);
            }

            notificationObjects.Clear();

            CollectNotifications();
            foreach (NotificationInfo notification in notificationInfos)
            {
                GameObject tmp = Instantiate(NotificationElement, NotificationElementParent.transform);
                NotificationElement element = tmp.GetComponent<NotificationElement>();
                element.TitleText.text = notification.title;
                element.DescriptionText.text = notification.description;
                notificationObjects.Add(tmp);
            }

            StartCoroutine(delayAnimation());
        }

        private void CollectNotifications()
        {
            notificationInfos = new List<NotificationInfo>();
            foreach (var info in Links.DeviceInformation.PlayerEvents)
            {
                NotificationInfo ni = new NotificationInfo();
                ni.title = info.title;
                ni.description = info.start.ToShortDateString();
                notificationInfos.Add(ni);
            } 
        }

        IEnumerator delayAnimation()
        { 
            yield return new WaitForSeconds(0.1f);
            if (notificationObjects.Count > 0)
            { 
                notificationObjects[0].SetActive(false);
                yield return new WaitForSeconds(0.1f);
                notificationObjects[0].SetActive(true); 
            }

            yield return null;
        }
    }
}