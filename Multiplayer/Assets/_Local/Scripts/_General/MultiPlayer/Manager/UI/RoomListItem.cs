using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace HammerRun.Pun.Managers
{
    public class RoomListItem : MonoBehaviour
    {
        public Text RoomName;
        public RoomInfo RoomData;
        public RoomListItem(RoomInfo info)
        {
            RoomName.text = info.Name;
            RoomData = info;
        }
        public void OnClick() => StartSceneHandler.Current.JoinRoom(RoomData);
    }
}