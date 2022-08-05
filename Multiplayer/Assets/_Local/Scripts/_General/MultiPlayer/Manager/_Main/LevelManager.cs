using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using HammerRun.Pun.Managers.UI;

namespace HammerRun.Pun.Managers
{
	public class LevelManager : MonoBehaviourPunCallbacks
	{
		public static LevelManager Current;

		#region Calls on otherPlayerAction, this player actions 
		public event Action<PlayerManagerPun>					OnLevelStart,
															    OnLevelExit,
															    OnLevelExited;

		public delegate void OnLeavelevel(GameObject go);
		public delegate void OnEnterRoom(string name);
		public delegate void OnLobbyJoined();
		public delegate void DisplayMessage(string msg, float time);

		public static event OnLeavelevel ExitingRoom;
		public static event OnEnterRoom EnteredRoom;
		public static event OnLobbyJoined JoinedLobby;

		#endregion

		#region UI Messages
		public GameEvents GameEvents;
		private GameManagerPun game => GameManagerPun.Current;
		private PlayerManagerPun player => FindObjectOfType<PlayerManagerPun>();
        #endregion
		private void Awake()
		{
			if (Current)
			{
				Destroy(gameObject);
				return;
			}
			DontDestroyOnLoad(gameObject);
			Current = this;
		}
		public override void OnEnable()
		{
			base.OnEnable();
			SceneManager.sceneLoaded += OnSceneLoaded;
		}
		public override void OnDisable()
		{
			base.OnDisable();
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
        
		[PunRPC]
		private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			if (scene.buildIndex == 1)
				game.InstantiateInPrefabs("RobotKyleForTesting", new Vector3(UnityEngine.Random.Range(5,10),
																			5,
																			UnityEngine.Random.Range(0, 5)));

			EnteredRoom(player.photonView.Owner.NickName);
			//MsgDisplayTime($"{player.photonView.Owner.NickName} Entered Room", 5);
			player.photonView.RPC("OnRemotePlayerEnteredScene", RpcTarget.All, $"{player.photonView.Owner.NickName}");
		}
	}
}