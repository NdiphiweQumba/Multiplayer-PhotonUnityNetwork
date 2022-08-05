using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System;
using System.IO;
using System.Collections.Generic;

namespace HammerRun.Pun.Managers
{
    public class GameManagerPun : MonoBehaviourPunCallbacks
	{ 
		#region Singleton GameManager
		private static GameManagerPun _current;
		public static GameManagerPun Current 
		{ 
			get 
			{
				return _current;
			} 
		}
		#endregion End Singleton 

		#region Game Events Actions
		public static event Action<Player> OnRemoteEnteredRoom;

		public delegate void OnExitingRoom(string msg);
		public delegate void OnEnteredRoom(string msg);
		public delegate void OnShowMessage(string msg, float time);

		public delegate void OnExitClick (GameObject go);

		public  OnExitingRoom    E_ExitingRoom;
		public  OnEnteredRoom    E_EnteredRoom;
		public  OnShowMessage    E_ShowPlayerMessage;
		#endregion

		#region  Public Fields
		[HideInInspector]
		public string PlayerName;
		[HideInInspector]
		public int PlayerHealth;
		[HideInInspector]
		public int PlayerScore;
		#endregion End Public Fields

        #region Private Fields
		[SerializeField]
		private GameObject playerPrefab;

		[SerializeField]
		private bool Debugging;
		#endregion End Private Fields

		#region MonoBehaviour CallBacks
		private void Awake()
        {
			if (_current)
			{
				Destroy(gameObject);
				return;
			}
			else
			{
				DontDestroyOnLoad(gameObject);
			}
			_current = this;
			PlayerName = PhotonNetwork.NickName;
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape)) { QuitApplication(); };
		}
		#endregion ------End MonoBehaviour CallBacks

		#region MonoBehaviour PUN Callbacks
		public override void OnPlayerEnteredRoom(Player player)
		{
			Debug.LogFormat($"Player with name {player.NickName} has joined the room");
        }
        public override void OnPlayerLeftRoom(Player other)
		{
			Debug.LogFormat($"Player with name {other.NickName} has left the room");
		}
		public override void OnLeftRoom()
		{
			E_ExitingRoom(PlayerName);
		}
		#region Public Methods
		public void InstantiateInPrefabs(string prefabName, Vector3 pos)
		{
			PhotonNetwork.Instantiate(Path.Combine("Prefabs", prefabName), pos, Quaternion.identity);
		}

		public override void OnFriendListUpdate(List<FriendInfo> friendsInfo)
		{
			for (int i = 0; i < friendsInfo.Count; i++)
			{
				FriendInfo friend = friendsInfo[i];
			}
		}
		#endregion

		#endregion

		#region Expression-Bodied Methods
		public  void QuitApplication()	=> Application.Quit();
		public bool OnlineFriends(string[] friendsUserIds) => PhotonNetwork.FindFriends(friendsUserIds);
		#endregion End Expression Bodied Methods
	}
}