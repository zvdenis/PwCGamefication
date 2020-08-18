using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Globalization;
using GameLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace UI_scripts
{
    public class RegisterEventLayout : MonoBehaviour
    {
        public InputField titleText;
        public InputField descriptionText;
        public Text timeText;
        public Text rewardText;
        public GameObject elementPrefab;
        public GameObject elementPrefabAdminParent;
        public GameObject elementPrefabManagerParent;

        public string CreateEventMessage;


        private List<PlayerData> admins = new List<PlayerData>();
        private List<PlayerData> managers = new List<PlayerData>();

        private List<GameObject> adminsElements = new List<GameObject>();
        private List<GameObject> managersElements = new List<GameObject>();

        private DateTime startTime;
        private DateTime endTime;


        private void OnEnable()
        {
            titleText.text = "";
            timeText.text = DateTime.Now.ToString();
            descriptionText.text = "";
            rewardText.text = "1";
            RemoveAdminsManagers();
        }

        private void RemoveAdminsManagers()
        {
            admins.Clear();
            foreach (var admin in adminsElements)
            {
                Destroy(admin);
            }

            managers.Clear();

            foreach (var manager in managersElements)
            {
                Destroy(manager);
            }
        }

        public void AddManagersClicked()
        {
            PlayerWrapper wrapper = new PlayerWrapper();
            wrapper.Status = PlayerWrapper.PlayerStatus.Manager;
            Links.FindUserLayout.Show(wrapper);
        }

        public void AddAdminsClicked()
        {
            PlayerWrapper wrapper = new PlayerWrapper();
            wrapper.Status = PlayerWrapper.PlayerStatus.Administrator;
            Links.FindUserLayout.Show(wrapper);
        }

        public void AddUser(PlayerWrapper playerWrapper)
        {
            switch (playerWrapper.Status)
            {
                case PlayerWrapper.PlayerStatus.Manager:
                    managers.Add(playerWrapper.PlayerData);
                    GameObject tmp = Instantiate(elementPrefab, elementPrefabManagerParent.transform);
                    adminElementController controller = tmp.GetComponent<adminElementController>();
                    controller.PlayerWrapper = playerWrapper;
                    playerWrapper.PlayerObject = tmp;
                    controller.Text = playerWrapper.PlayerData.NickName;
                    managersElements.Add(tmp);
                    LayoutRebuilder.ForceRebuildLayoutImmediate(elementPrefabManagerParent
                        .GetComponent<FlowLayoutGroup>().RectTransform);
                    break;
                case PlayerWrapper.PlayerStatus.Administrator:
                    admins.Add(playerWrapper.PlayerData);
                    GameObject tmp2 = Instantiate(elementPrefab, elementPrefabAdminParent.transform);
                    adminElementController controller2 = tmp2.GetComponent<adminElementController>();
                    controller2.PlayerWrapper = playerWrapper;
                    playerWrapper.PlayerObject = tmp2;
                    controller2.Text = playerWrapper.PlayerData.NickName;
                    adminsElements.Add(tmp2);
                    LayoutRebuilder.ForceRebuildLayoutImmediate(elementPrefabAdminParent.GetComponent<FlowLayoutGroup>()
                        .RectTransform);
                    break;
            }
        }

        private void ParseTime()
        {
            string time = timeText.text;
            CultureInfo timeFormat = CultureInfo.GetCultureInfo("ru-RU");
            startTime = DateTime.Parse(time, timeFormat);
            
            //Links.ToastController.Show(timeText.text + "!!D" + startTime.Day + "M" + startTime.Month);
        }

        public void CreateEventButtonClicked()
        {
            CreateEvent();
        }

        public void CreateEvent()
        {
            ParseTime();
            EventData data = new EventData(-1, startTime, startTime, titleText.text, descriptionText.text, 1,
                Int32.Parse(rewardText.text), admins, managers);
            Links.RequestController.RegisterEvent(data);
            gameObject.SetActive(false);
            Links.ToastController.Show(CreateEventMessage);
        }

        public void OnTimeSelectEvent(object result)
        {
            timeText.text += "   " + result;
            DateTime parsed = DateTime.Parse(result.ToString());
            //startTime.AddMinutes(parsed.Minute);
            //startTime.AddHours(parsed.Hour);
        }

        public void OnDateSelectEvent(object result)
        {
            timeText.text = result.ToString();
            //startTime = DateTime.Parse(result);
        }

        public void DeletePlayer(PlayerWrapper playerWrapper)
        {
            switch (playerWrapper.Status)
            {
                case PlayerWrapper.PlayerStatus.Administrator:
                    adminsElements.Remove(playerWrapper.PlayerObject);
                    admins.Remove(playerWrapper.PlayerData);
                    Destroy(playerWrapper.PlayerObject);
                    break;

                case PlayerWrapper.PlayerStatus.Manager:
                    managersElements.Remove(playerWrapper.PlayerObject);
                    managers.Remove(playerWrapper.PlayerData);
                    Destroy(playerWrapper.PlayerObject);
                    break;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}