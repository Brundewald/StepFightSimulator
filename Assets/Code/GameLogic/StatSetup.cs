using System;
using FightSimulator.NPC;

namespace FightSimulator
{
    public class StatSetup
    {
        private readonly StatSetType _type;
        private int _LHPoint;
        private int _RHPoint;
        private int _LUBPoint;
        private int _RUBPoint;
        private int _LMBPoint;
        private int _RMBPoint;
        private int _LLBPoint;
        private int _RLBPoint;

        public int LHPoint
        {
            get => _LHPoint;
            set
            {
                if (value != LHPoint)
                {
                    _LHPoint = value;
                    OnPropertyChange.Invoke(nameof(LHPoint), _LHPoint);
                }
            }
        }
        
        public int RHPoint
        {
            get => _RHPoint;
            set
            {
                if (value != RHPoint)
                {
                    _RHPoint = value;
                    OnPropertyChange.Invoke(nameof(RHPoint), _RHPoint);
                }
            }
        }
        
        public int LUBPoint
        {
            get => _LUBPoint;
            set
            {
                if (value != LUBPoint)
                {
                    _LUBPoint = value;
                    OnPropertyChange.Invoke(nameof(LUBPoint), _LUBPoint);
                }
            }
        }
        
        public int RUBPoint
        {
            get => _RUBPoint;
            set
            {
                if (value != RUBPoint)
                {
                    _RUBPoint = value;
                    OnPropertyChange.Invoke(nameof(RUBPoint), _RUBPoint);
                }
            }
        }
        
        public int LMBPoint
        {
            get => _LMBPoint;
            set
            {
                if (value != LMBPoint)
                {
                    _LMBPoint = value;
                    OnPropertyChange.Invoke(nameof(LMBPoint), _LMBPoint);
                }
            }
        }
        
        public int RMBPoint
        {
            get => _RMBPoint;
            set
            {
                if (value != RMBPoint)
                {
                    _RMBPoint = value;
                    OnPropertyChange.Invoke(nameof(RMBPoint), _RMBPoint);
                }
            }
        }
        
        public int LLBPoint
        {
            get => _LLBPoint;
            set
            {
                if (value != LLBPoint)
                {
                    _LLBPoint = value;
                    OnPropertyChange.Invoke(nameof(LLBPoint), _LLBPoint);
                }
            }
        }
        
        public int RLBPoint
        {
            get => _RLBPoint;
            set
            {
                if (value != RLBPoint)
                {
                    _RLBPoint = value;
                    OnPropertyChange.Invoke(nameof(RLBPoint), _RLBPoint);
                }
            }
        }

        public StatSetType Type => _type;

        public event Action<string, int> OnPropertyChange = delegate(string name, int value) {  };
        
        public StatSetup(StatSetType type)
        {
            _type = type;
        }

        public void UpdateStat(PointType type)
        {
            switch (type)
            {
                case PointType.LH:
                    LHPoint++;
                    break;
                case PointType.RH:
                    RHPoint++;
                    break;
                case PointType.LUB:
                    LUBPoint++;
                    break;
                case PointType.RUB:
                    RUBPoint++;
                    break;
                case PointType.LMB:
                    LMBPoint++;
                    break;
                case PointType.RMB:
                    RMBPoint++;
                    break;
                case PointType.LLB:
                    LLBPoint++;
                    break;
                case PointType.RLB:
                    RLBPoint++;
                    break;
            }
        }

        public void SetupNPC(StatPreset preset)
        {
            LHPoint = preset.LH;
            RHPoint = preset.RH;
            LUBPoint = preset.LUB;
            RUBPoint = preset.RUB;
            LMBPoint = preset.LMB;
            RMBPoint = preset.RMB;
            LLBPoint = preset.LLB;
            RLBPoint = preset.RLB;
        }
    }
}