using System;
using TMPro;
using UnityEngine;

namespace FightSimulator
{
    public class PrepareMessageScreenView: MonoBehaviour
    {
        private const string Message = "FIGHTER %value%";
        
        [SerializeField] private TextMeshProUGUI _message;
        private int _counter;
        private bool _isSinglePlayer;

        public void SetMode(bool isSinglePLayer)
        {
            _isSinglePlayer = isSinglePLayer;
            SetMessage();
        }
        
        private void OnEnable()
        {
            SetMessage();
        }

        private void SetMessage()
        {
            if (_isSinglePlayer)
            {
                _message.text = Message.Replace("%value%", "");
            }
            else
            {
                _counter++;
                UpdateMessage(_counter);
            }
        }

        private void UpdateMessage(int value)
        {
            _message.text = Message.Replace("%value%", value % 2==1 ? "1" : "2");
        }
    }
}