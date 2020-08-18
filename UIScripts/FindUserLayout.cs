using System.Collections.Generic;
using GameLibrary; 
using UnityEngine;
using UnityEngine.UI;

namespace UI_scripts
{
    public class FindUserLayout : MonoBehaviour
    {
        public GameObject PlayerElementPrefab;
        public GameObject PlayerElementPrefabParent;
        public GameObject LoadingElement;
        private List<GameObject> PlayerList = new List<GameObject>();
        public Text InputText;

        private PlayerWrapper _playerWrapper;

        private void OnEnable()
        {
            LoadingElement.SetActive(false);
//            List<PlayerData> admins = new List<PlayerData>();
//            PlayerData a = new PlayerData(0, 0, "Den", 0, 0, 0, "");
//            PlayerData b = new PlayerData(0, 0, "DenMuden", 0, 0, 0, "");
//            PlayerData c = new PlayerData(0, 0, "MudenDenMuden", 0, 0, 0, "");
//            admins.Add(a);
//            admins.Add(b);
//            admins.Add(c);
//            UpdateList(admins);
        }

        public void UpdateList(List<PlayerData> datas)
        {
            foreach (var data in datas)
            {
                GameObject tmp = Instantiate(PlayerElementPrefab, PlayerElementPrefabParent.transform);
                PlayerList.Add(tmp);
                TextChanger textChanger = tmp.GetComponent<TextChanger>();
                textChanger.playerData = data;
                textChanger.Text = data.NickName;
            }

            LoadingElement.SetActive(false);
        }

        public void ChoosePlayer(PlayerData data)
        {
            _playerWrapper.PlayerData = data;
            Links.RegisterEventLayout.AddUser(_playerWrapper);
            Hide();
        }

        public void FindButtonClicked() 
        {
            LoadingElement.SetActive(true);
            HidePlayerList();
            Links.RequestController.RequestPlayersByName(InputText.text);
        }

        public void Show(PlayerWrapper wrapper)
        {
            _playerWrapper = wrapper;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            LoadingElement.SetActive(false);
            HidePlayerList();
        }

        private void HidePlayerList()
        {
            foreach (var player in PlayerList)
            {
                Destroy(player);
            }
        }
    }
}