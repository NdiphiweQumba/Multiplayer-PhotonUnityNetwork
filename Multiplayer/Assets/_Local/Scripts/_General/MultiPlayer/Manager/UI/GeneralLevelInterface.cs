using UnityEngine;
using UnityEngine.UI;

namespace HammerRun.Pun.Managers
{
    public class GeneralLevelInterface : MonoBehaviour
    {
        public GameText[] GameText;
    }

    [System.Serializable]
    public class GameText
    {
        public string Name;
        public Text TextDisplay;
        public string MessageText;
        public GameText(Text txt, string msg)
        {
            TextDisplay = txt;
            MessageText = msg;
        }
        public void MessageClear(Text txt)
        {
            txt.text = string.Empty;
        }

        public string GetTextName
        {
            get { return Name; }

            private set
            {
                if (char.IsDigit(Name, Name.Length))
                    return;
                Name = value;
            }
        }

        public string setMessage
        {
            get { return MessageText; }
        }
    }
   
}