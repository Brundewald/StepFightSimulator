using TMPro;
using UnityEngine;

namespace FightSimulator
{
    public class WinnerMessageView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageField;

        public void SetMessage(string message)
        {
            _messageField.text = message;
        }
    }
}