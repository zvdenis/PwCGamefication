using GameLibrary;
using UnityEngine;

namespace UI_scripts
{
    public class PlayerWrapper
    {
        public PlayerData PlayerData;
        public GameObject PlayerObject;
        public PlayerStatus Status;

        public enum PlayerStatus
        {
            Administrator,
            Manager
        }
    }
}