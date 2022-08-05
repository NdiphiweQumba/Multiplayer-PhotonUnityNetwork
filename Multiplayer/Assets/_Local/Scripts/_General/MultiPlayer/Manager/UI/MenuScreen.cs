using UnityEngine.UI;
using UnityEngine;


namespace HammerRun.Pun.Managers.UI
{
    public class MenuScreen : MonoBehaviour
    {
        #region Public Variables
        public string MenuName;
        public bool IsActive;
        #endregion End Public Variables

        #region  Public Methods
        public void OpenMenu()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }
        public void CloseMenu()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
        #endregion End public Methods
    }
}