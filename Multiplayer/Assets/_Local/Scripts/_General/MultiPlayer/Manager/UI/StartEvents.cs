using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using HammerRun.Pun.Managers.UI;
using System.Collections.Generic;

namespace HammerRun.Pun.Managers
{
    public class StartEvents : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private List<MenuScreen> Menu;
        #endregion End Fields

        #region Unity Callbacks 
        private void Awake()
        {
            StartSceneHandler.Connected += JoinLobby;
            StartSceneHandler.LevelLoad += StartLevel;

            StartSceneHandler.OnJoinRoom += OnJoinRoom;
            StartSceneHandler.JoinRand += OnJoinRandomRoom;

            StartSceneHandler.StartPressed += SetName;
            /// Menus 
            StartSceneHandler.OpenScreenMenu += OpenScreen;
            StartSceneHandler.OnOpenScreenName += OpenScreen;
            StartSceneHandler.OnClosingScreen += CloseMenu;
        }
        private void Start()
        {
            MenuScreen[] menuitems = FindObjectsOfType<MenuScreen>(true);
            foreach (var mi in menuitems)
                Menu.Add(mi);
        }
        #endregion Unity Callbacks  

        #region Photon Callbacks 
        private void JoinLobby() => PhotonNetwork.JoinLobby();
        private void OnJoinRoom(RoomInfo info) => PhotonNetwork.JoinRoom(info.Name);
        private void OnJoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
        private void StartLevel(int l) => PhotonNetwork.LoadLevel(l);
        private void SetName(string player)
        {
            string defaultName = string.Empty;
            if (string.IsNullOrEmpty(player))
            {
                Debug.LogError("Player Name is null or empty"); /// Give Error  then return /// 
                return;
            }
            PlayerPrefs.SetString(StartSceneHandler.PLAYER_NAME, player);
            PhotonNetwork.NickName = player;
        }

        public void OpenScreen(string name)
        {
            for (int i = 0; i < Menu.Count; i++)
            {
                if (Menu[i].MenuName == name)
                {
                    Menu[i].OpenMenu();
                }
                else if (Menu[i].IsActive)
                {
                    Menu[i].CloseMenu();
                }
            }
        }
        public void OpenScreen(MenuScreen menu)
        {
            for (int i = 0; i < Menu.Count; i++)
            {
                if (Menu[i].IsActive)
                {
                    Menu[i].CloseMenu();
                }
            }
            menu.OpenMenu();
        }
        public void CloseMenu(MenuScreen menu) => menu.CloseMenu();

        #endregion
    }
}