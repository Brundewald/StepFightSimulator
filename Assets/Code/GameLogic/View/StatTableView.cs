using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FightSimulator
{
    public class StatTableView : MonoBehaviour
    {
        [Header("Stat parameters")] 
        [SerializeField] private TextMeshProUGUI _statTableName;
        [SerializeField] private StatSetType _statSetType;
        [SerializeField] private int _baseStatPoints;
        
        [Header("Stat points fields")]
        [SerializeField] private TextMeshProUGUI _LHField;
        [SerializeField] private TextMeshProUGUI _RHField;
        [SerializeField] private TextMeshProUGUI _LUBField;
        [SerializeField] private TextMeshProUGUI _RUBField;
        [SerializeField] private TextMeshProUGUI _LMBField;
        [SerializeField] private TextMeshProUGUI _RMBField;
        [SerializeField] private TextMeshProUGUI _LLBField;
        [SerializeField] private TextMeshProUGUI _RLBField;

        [Header("Stat buttons")]
        [SerializeField] private Button _LHButton;
        [SerializeField] private Button _RHButton;
        [SerializeField] private Button _LUBButton;
        [SerializeField] private Button _RUBButton;
        [SerializeField] private Button _LMBButton;
        [SerializeField] private Button _RMBButton;
        [SerializeField] private Button _LLBButton;
        [SerializeField] private Button _RLBButton;
        
        private StatSetup _statSetup;


        private int _statPoints;
        public int StatPoints
        {
            get => _statPoints;
            set
            {
                if (_statPoints != value)
                {
                    _statPoints = value;
                    StatPointChange.Invoke();
                }
            }
        }
        
        public event Action StatPointChange = delegate { };

        public StatSetup GetStatSetup()
        {
            return _statSetup;
        }

        private void Awake()
        {
            StatPointChange += UpdatePoints;
        }

        private void OnEnable()
        {
            ChangeButtonsState(true);
            StatPoints = _baseStatPoints;
            _statSetup = new StatSetup(_statSetType);
            SubscribeToEvents();
            ResetCounters();
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        private void OnDestroy()
        {
            StatPointChange -= UpdatePoints;
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _statSetup.OnPropertyChange += UpdateStat;
            _LHButton.onClick.AddListener(() => AddPoint(PointType.LH, _LHButton));
            _RHButton.onClick.AddListener(() => AddPoint(PointType.RH, _RHButton));
            _LUBButton.onClick.AddListener(() => AddPoint(PointType.LUB, _LUBButton));
            _RUBButton.onClick.AddListener(() => AddPoint(PointType.RUB, _RUBButton));
            _LMBButton.onClick.AddListener(() => AddPoint(PointType.LMB, _LMBButton));
            _RMBButton.onClick.AddListener(() => AddPoint(PointType.RMB, _RMBButton));
            _LLBButton.onClick.AddListener(() => AddPoint(PointType.LLB, _LLBButton));
            _RLBButton.onClick.AddListener(() => AddPoint(PointType.RLB, _RLBButton));
        }

        private void UnsubscribeToEvents()
        {
            _statSetup.OnPropertyChange -= UpdateStat;
            _LHButton.onClick.RemoveAllListeners();
            _RHButton.onClick.RemoveAllListeners();
            _LUBButton.onClick.RemoveAllListeners();
            _RUBButton.onClick.RemoveAllListeners();
            _LMBButton.onClick.RemoveAllListeners();
            _RMBButton.onClick.RemoveAllListeners();
            _LLBButton.onClick.RemoveAllListeners();
            _RLBButton.onClick.RemoveAllListeners();
        }

        private void UpdatePoints()
        {
            _statTableName.text = $"{_statSetType}: {StatPoints}";
        }

        private void UpdateStat(string name, int value)
        {
            if (name.Equals("LHPoint"))
            {
                _LHField.text = value.ToString();
            }
            if (name.Equals("RHPoint"))
            {
                _RHField.text = value.ToString();
            }
            if (name.Equals("LUBPoint"))
            {
                _LUBField.text = value.ToString();
            }
            if (name.Equals("RUBPoint"))
            {
                _RUBField.text = value.ToString();
            }
            if (name.Equals("RMBPoint"))
            {
                _RMBField.text = value.ToString();
            }
            if (name.Equals("LMBPoint"))
            {
                _LMBField.text = value.ToString();
            }
            if (name.Equals("LLBPoint"))
            {
                _LLBField.text = value.ToString();
            }
            if (name.Equals("RLBPoint"))
            {
                _RLBField.text = value.ToString();
            }
        }

        private void AddPoint(PointType type, Button button)
        {
            StatPoints--;
            UpdatePoints();
                
            if (StatPoints > 0)
            {
                _statSetup.UpdateStat(type);
                DeactivateButton(button);
            }
            else
            {
                _statSetup.UpdateStat(type);
                DeactivateButton(button);
                ChangeButtonsState(false);
            }
        }

        private void DeactivateButton(Button button)
        {
            if (_statSetup.Type == StatSetType.DEFENCE)
            {
                button.interactable = false;
            }
        }

        private void ResetCounters()
        {
            _LHField.text = "0";
            _RHField.text = "0";
            _LUBField.text = "0";
            _RUBField.text = "0";
            _LMBField.text = "0";
            _RMBField.text = "0";
            _LLBField.text = "0";
            _RLBField.text = "0";
        }

        private void ChangeButtonsState(bool state)
        {
            _LHButton.interactable = state;
            _RHButton.interactable = state;
            _LUBButton.interactable = state;
            _RUBButton.interactable = state;
            _LMBButton.interactable = state;
            _RMBButton.interactable = state;
            _LLBButton.interactable = state;
            _RLBButton.interactable = state;
        }
    }
}