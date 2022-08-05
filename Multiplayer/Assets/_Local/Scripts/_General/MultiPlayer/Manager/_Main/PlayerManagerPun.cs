using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.EventSystems;
using HammerRun.Pun.Managers.UI;

namespace HammerRun.Pun.Managers
{
    public class PlayerManagerPun : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields
        public float PlayerHealth = 1f;
        public GameEvents eventsgame;
        public static GameObject PlayerInstanceLocal;

        #endregion

        #region Private Fields
        [SerializeField]
        private GameObject playerInfo;
        [SerializeField]
        private GameObject beams;
        bool IsFiring;

        #endregion

        #region MonoBehaviour CallBacks
        public override void OnEnable()
        {
            base.OnEnable();

        }
        private void Awake()
        {
            if (this.beams != null)
                this.beams.SetActive(false);
            PlayerInstanceLocal = photonView.IsMine ? gameObject : null;
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            CameraWork _cameraWork = gameObject.GetComponent<CameraWork>(); //  Demo Camera to Delete //

            if (photonView.IsMine && _cameraWork != null)
                _cameraWork.OnStartFollowing();

        }
        public void Update()
        {
            if (photonView.IsMine)
            {
                this.ProcessInputs();

                if (this.PlayerHealth <= 0f)
                {
                   /// ExitRooom //// 
                }
            }
            if (beams != null && IsFiring != beams.activeInHierarchy)
                beams.SetActive(IsFiring);
        }

        public void ReportOn(string name)
        {
            Debug.Log($"Player {name} is in the same room");
            FindObjectOfType<GameEvents>().DisplayMessage($"Player {name} is in the same room", 10f);
        }
        [PunRPC]
        public void OnRemotePlayerEnteredScene(PlayerManagerPun player)
        { 
            
        }

        private void LeaveRoom() { }
        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }

            PlayerHealth -= 0.1f;
        }
        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            this.PlayerHealth -= 0.1f * Time.deltaTime;
        }
        #endregion

        #region Private Methods
        private void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //	return;
                }

                if (!this.IsFiring)
                {
                    this.IsFiring = true;
                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                if (this.IsFiring)
                {
                    this.IsFiring = false;
                }
            }
        }
        #endregion

        #region IPunObservable
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(IsFiring);
                stream.SendNext(PlayerHealth);
            }
            else
            {
                // Network player, receive data
                IsFiring = (bool)stream.ReceiveNext();
                PlayerHealth = (float)stream.ReceiveNext();
            }
        }
        #endregion
    }
}
