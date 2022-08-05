using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using Photon.Pun.Demo.PunBasics;

namespace HammerRun.Pun.Managers.UI
{
    public class GameEvents : MonoBehaviour
    {
        #region Public Fields
        [Header("Info About Player")]
        [SerializeField]
        private Text playerName;
        [SerializeField]
        private Text playerScore;
        [SerializeField] 
        private Text playerHp;
        [SerializeField] 
        private Text playerMessage;

        PlayerManager target;
        #endregion

        #region Monobehavior Callbacks
        private void Awake() 
        {
            LevelManager.EnteredRoom += DisplayPlayerName;
         
            /// Local Player Actions /// 
        }
         #endregion

        #region General palyer and Enemy Messages
        private void EnteredRoom(string playerName)
        {
            Debug.Log($"Player {playerName} Entered Room");
            this.playerName.text = playerName;
        }
        private void ExitRoom(string player)
        {
            /// Exitroom Clicked
            /// Prompt player if they want to leave
            /// Distroy Player Game Object
            /// Update Player List /// Update player Count
            /// Display PlayerLeft Room 
        }
        #endregion

        #region  User Interface (Display) 
        private void DisplayPlayerName(string name) => playerName.text   = name;
        private void UpdatePlayerHealth(int value)   => playerName.text   = value.ToString();
        private void DisPlayPlayerLevel(int value)   => playerName.text   = value.ToString();

        #endregion

        #region Common Functions
        private IEnumerator WaitSeconds(float time, System.Action act)
        {
            yield return new WaitForSeconds(time);
            act.Invoke();
        }
        public void DisplayMessage(string value, float time)
        {
            playerMessage.text = value;
            StartCoroutine(WaitSeconds(time, ClearMessage));
        }
        private void ClearMessage() =>  playerMessage.text = string.Empty;
        private void LoadStartScene(string l) => PhotonNetwork.LoadLevel(1);
        #endregion
    }
}
