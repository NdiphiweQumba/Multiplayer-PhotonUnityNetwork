using UnityEngine.UI;
using UnityEngine;
using System;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using HammerRun.Pun.Managers.UI;
using System.Collections;

namespace HammerRun.Pun.Managers
{
	public class StartSceneHandler : MonoBehaviourPunCallbacks
	{
		private static StartSceneHandler _current;
		public static StartSceneHandler Current { get; private set; }

		public delegate void OnStartConnection();
		public static event OnStartConnection StartConnection;
		
		public delegate void OnMasterConnected();
		public static event OnMasterConnected Connected;

		public static event Action<int> LevelLoad;
		public static event Action<MenuScreen> OpenScreenMenu;
		public static event Action<string>     OnOpenScreenName;
		public static event Action<MenuScreen> OnClosingScreen;

		public static event Action<string> _LoadScreen;
		public static event Action<int> _Pause;

		public static event Action<RoomInfo> OnJoinRoom;
		public static event Action<RoomInfo> OnRoomCreation;
		public static event Action JoinRand;

		public static event Action _LeaveRoom;
		public static event Action _MasterConnect;
		public static event Action<string> StartPressed;
		public static event Action<short, string> _FailConnection;



		#region Private Serializable Fields
		[SerializeField]
		public Text PlayerName;
		[SerializeField]
		private Text PlayerMessage;
		[SerializeField]
		private LoaderAnime loaderAnime;
		[SerializeField]
		private Text RoomNameInputField;
		[SerializeField]
		private Text RoomName;
        #endregion End Serializable Fields

        #region Private Fields
        [HideInInspector]
		public const string PLAYER_NAME = "default";
        [SerializeField]
		private bool JoinRandom;
		private string gameVersion = "1";
		#endregion End Private Fields

		#region MonoBehaviour CallBacks
		private void Awake()
		{
			_current = this;
			if (loaderAnime == null)
				Debug.LogError("<Color=Red><b>Missing</b></Color> loaderAnime Reference.", this);
			PhotonNetwork.AutomaticallySyncScene = true;
			DisplayPlayerName(); // Display from the PlayerPrefs
		}
		#endregion

		#region Public Custom Methods
		public void LoadingScreen()
		{
			if (loaderAnime != null)
			{
				loaderAnime.StartLoaderAnimation(); // Loading thing
				OnOpenScreenName("LoadingScreen");
			}
		}
		public void ConnectToNetwork()
		{
			PlayerMessage.text = "";
			StartPressed(PlayerName.text);
			LoadingScreen();
			
			if (PhotonNetwork.IsConnected)
			{
				PlayerFeedback("Connected to the Network...");
				if (!JoinRandom)
					OnOpenScreenName("MainMenu");
				else
					OnOpenScreenName("JoinRandom");

				loaderAnime.StopLoaderAnimation();
			}
			else
			{
				PlayerFeedback("Connecting...");
				PhotonNetwork.ConnectUsingSettings();
				PhotonNetwork.GameVersion = this.gameVersion;
			}
		}
		public void JoinRoom(RoomInfo info)
		{
			PhotonNetwork.JoinRoom(info.Name);
			OnOpenScreenName("Loading");
		}
		public void CreateRoom()
		{
			if (!string.IsNullOrEmpty(RoomNameInputField.text))
			{
				PhotonNetwork.CreateRoom(RoomNameInputField.text);
				OnOpenScreenName("RoomCreated");
			}
			else
			{
				PlayerFeedback("Please Enter a room name", "#fff");
			}
		}
		public void StartGame() => LevelLoad(1);

		public void GameStartRandomRoom()
		{
			JoinRand();
			loaderAnime.StartLoaderAnimation();
		}
		#endregion

		#region MonoBehaviourPunCallbacks
		public override void OnConnectedToMaster()
		{
			Connected();
		}
		public override void OnJoinedLobby()
		{
			PlayerMessage.text = "Connected Joined Lobby...";
			PhotonNetwork.NickName = PlayerName.text;
			loaderAnime.StopLoaderAnimation();

			OnOpenScreenName(!JoinRandom ? "MainMenu" : "JoinRandom");
		}
		public override void OnJoinRandomFailed(short returnCode, string message)
		{
			PlayerFeedback("<color=red>OnJoinRandomFailed</color>: Next -> Create a new Room");
			PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 20 });
		}
		public override void OnDisconnected(DisconnectCause cause)
		{
			Clear(PlayerMessage);
			PlayerFeedback("<color=Red>Could not connect! Check Connection</color> " + cause);
			OnOpenScreenName("Retry");
			Debug.LogError($"{cause} : internet failer ;(");
			loaderAnime.StopLoaderAnimation();
		}
		public override void OnJoinedRoom()
		{
			loaderAnime.StopLoaderAnimation();
			RoomName.text = PhotonNetwork.CurrentRoom.Name;
			PhotonNetwork.LoadLevel(1);
		}
		public void LeaveRoom()
		{
			PhotonNetwork.LeaveLobby();
			PlayerFeedback($"Player {PlayerName.text} Just Left...");
		}

		#endregion End

		#region Private OverLoaded Functions
		private void DisplayPlayerName()
		{
			string defaultName = string.Empty;
			string input = PlayerName.text;

			if (input != null)
			{
				if (PlayerPrefs.HasKey(PLAYER_NAME))
				{
					defaultName      = PlayerPrefs.GetString(PLAYER_NAME);
					PlayerName.text  = defaultName;
				}
			}
			PhotonNetwork.NickName = defaultName;
		}
		private void PlayerFeedback(string message)
		{
			if (PlayerMessage == null)
				return;
			PlayerMessage.text += System.Environment.NewLine + message;
		}
		private void PlayerFeedback(string message, string col)
		{
			if (PlayerMessage == null)
				return;
			Color color;
			ColorUtility.TryParseHtmlString(col, out color);

			PlayerMessage.text += System.Environment.NewLine + message;
			PlayerMessage.GetComponent<Text>().color = color;
		}
		private void PlayerFeedback(string message, string col, float sec)
		{
			if (PlayerMessage == null)
				return;
			Color color;
			ColorUtility.TryParseHtmlString(col, out color);

			PlayerMessage.text += Environment.NewLine + message;
			PlayerMessage.GetComponent<Text>().color = color;
			StartCoroutine(Info_Event(sec, PlayerMessage, Clear));
			StartCoroutine(Info_Event(sec * 2, PlayerMessage, EnabledText));
		}
		public void Clear(Text t) => t.text = "";
		public void EnabledText(Text t) { t.enabled = true; t.text = "Hammer Run"; }
		#endregion

		#region System Collections 
		private IEnumerator Info_Event(float s, Text t, System.Action<Text> action = null)
		{
			yield return new WaitForSeconds(s);
			action(t);
		}
		#endregion
	} 
}


