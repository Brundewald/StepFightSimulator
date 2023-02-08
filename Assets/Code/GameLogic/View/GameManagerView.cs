using System.Collections.Generic;
using FightSimulator.NPC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FightSimulator
{
    public class GameManagerView : MonoBehaviour
    {
        [SerializeField] private NPCBehaviorData _npcBehavior;
        [SerializeField] private StatTableView _offenceStatTables;
        [SerializeField] private StatTableView _defenceStatTables;
        [SerializeField] private FightSceneHandlerView _fightSceneHandler;
        [SerializeField] private PrepareMessageScreenView prepareMessageScreen;
        [SerializeField] private float _timeToPrepare = 15;
        [SerializeField] private int _baseHealth = 10;
        [SerializeField] private int _damageModifier = 1;
        [SerializeField] private Button _readyButton;
        [SerializeField] private TextMeshProUGUI _firstFighterHP;
        [SerializeField] private TextMeshProUGUI _secondFighterHP;
        [SerializeField] private TextMeshProUGUI _prepareTimerField;

        private PrepareTimer _prepareTimer;
        private List<FighterData> _fighters;
        private bool _isSinglePlayer;
        private int _roundCounter;
        private int _prepareCount = 0;

        public void SetMode(bool isSingle)
        {
            _isSinglePlayer = isSingle;
        }

        public void SetForNextRound()
        {
            _prepareCount = 0;
            foreach (var fighter in _fighters)
            {
                fighter.Setup(null, null);
            }
        }

        public void Reset()
        {
            _prepareCount = 0;
            _fighters = new List<FighterData>() {new(_baseHealth, _damageModifier), new(_baseHealth, _damageModifier)};
        }

        private void Awake()
        {
            _prepareTimer = new PrepareTimer();
            _fighters = new List<FighterData>() {new(_baseHealth, _damageModifier), new(_baseHealth, _damageModifier)};
            _readyButton.onClick.AddListener(PlayerReady);
            _prepareTimer.TimerElapsed += StopPreparing;
            _prepareTimer.OnTimeChange += UpdateTimer;
            _offenceStatTables.StatPointChange += UpdateReadyButton;
            _defenceStatTables.StatPointChange += UpdateReadyButton;
        }

        private void OnEnable()
        {
            StartTimer();
            UpdateFightersHP();
        }

        private void OnDisable()
        {
            _prepareTimer.Stop();
        }

        private void UpdateFightersHP()
        {
            _firstFighterHP.text = $"{_fighters[0].HP} HP";
            _secondFighterHP.text = $"{_fighters[1].HP} HP";
        }

        private void OnDestroy()
        {
            _readyButton.onClick.RemoveAllListeners();
            _prepareTimer.TimerElapsed -= StopPreparing;
            _prepareTimer.OnTimeChange -= UpdateTimer;
            _offenceStatTables.StatPointChange -= UpdateReadyButton;
            _defenceStatTables.StatPointChange -= UpdateReadyButton;
        }

        private void UpdateReadyButton()
        {
            _readyButton.interactable = _offenceStatTables.StatPoints == 0 && _defenceStatTables.StatPoints == 0;
        }

        private void PlayerReady()
        {
            _prepareTimer.Stop();
            StopPreparing();
        }

        private void UpdateTimer(float time)
        {
            _prepareTimerField.text = $"TIME LEFT: {time: 00} sec";
        }

        private void StartTimer()
        {
            _prepareTimer.StartTimer(_timeToPrepare);
        }

        private void StopPreparing()
        {
            if(_prepareCount < 2)
            {
                SetupPlayer();

                if (_isSinglePlayer)
                {
                    _fighters[_prepareCount + 1].Setup(_npcBehavior.GetOffenceStat(), _npcBehavior.GetDefenceStat());
                }

                if (!_isSinglePlayer && _prepareCount == 0)
                {
                    ShowMessage();
                }
                
                else
                {
                    StartFight();
                }
                _prepareCount++;
            }
        }

        private void SetupPlayer()
        {
            var offenceSetup = _offenceStatTables.GetStatSetup();
            var defenceSetup = _defenceStatTables.GetStatSetup();
            _fighters[_prepareCount].Setup(offenceSetup, defenceSetup);
        }

        private void ShowMessage()
        {
            prepareMessageScreen.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void StartFight()
        {
            gameObject.SetActive(false);
            _fightSceneHandler.InitiateFight(_fighters);
        }
    }
}
