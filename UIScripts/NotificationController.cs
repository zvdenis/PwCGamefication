using System;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Playables;

namespace UI_scripts
{
    public class NotificationController : MonoBehaviour
    {
        private AndroidNotificationChannel standardNotificationChannel;
        void Start()
        {
            Links.NotificationController = this;
            
            standardNotificationChannel = new AndroidNotificationChannel()
            
            {
                Id = "standard",
                Name = "standardChannel",
                Importance = Importance.Default,
                Description = "notification",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(standardNotificationChannel);
        }

        public void SendAndroidNotification(string title, string text, System.DateTime fireTime)
        {
            var notification = new AndroidNotification();
            notification.Text = text;
            notification.Title = title;
            notification.FireTime = fireTime;
            notification.LargeIcon = "icon_1";
            notification.SmallIcon = "icon_0";
            AndroidNotificationCenter.SendNotification(notification, standardNotificationChannel.Id);
        }

        
    }
}
