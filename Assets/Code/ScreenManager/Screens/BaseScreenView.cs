using System;
using UnityEngine;
using UnityEngine.UI;

namespace FightSimulator
{
    public class BaseScreenView : MonoBehaviour
    {
        [SerializeField] private ScreenType _currentScreen;
        [SerializeField] private ScreenType _switchToScreen;
        [SerializeField] private Button _switchScreenButton;

        public ScreenType Type => _currentScreen;
        public Action<ScreenType> SwitchScreen = delegate {};
        
        public virtual void SwitchScreenTo()
        {
            SwitchScreen.Invoke(_switchToScreen);
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _switchScreenButton.onClick.AddListener(SwitchScreenTo);
        }

        private void OnDisable()
        {
            _switchScreenButton.onClick.RemoveAllListeners();
        }
    }
}
