using System.Collections.Generic;

namespace FightSimulator
{
    public class FighterData
    {
        private readonly List<OffenceResultData> _fightResult;
        private readonly int _damageModifier;
        private StatSetup _offenceData;
        private StatSetup _defenceData;
        private int _hp;
        
        public StatSetup Offence => _offenceData;

        public bool Ready => _offenceData != null && _defenceData != null;
        public int HP => _hp;

        public FighterData(int baseHealth, int damageModifier)
        {
            _fightResult = new List<OffenceResultData>();
            _damageModifier = damageModifier;
            _hp = baseHealth;
        }
        
        public void Setup(StatSetup offenceData, StatSetup defenceData)
        {
            _offenceData = offenceData;
            _defenceData = defenceData;
        }

        public List<OffenceResultData> CheckAttackResult(StatSetup offence)
        {
            _fightResult.Clear();
            var LHAttackResult = _defenceData.LHPoint < offence.LHPoint;
            var RHAttackResult = _defenceData.RHPoint < offence.RHPoint;
            var LUBAttackResult = _defenceData.LUBPoint < offence.LUBPoint;
            var RUBAttackResult = _defenceData.RUBPoint < offence.RUBPoint;
            var LMBAttackResult = _defenceData.LMBPoint < offence.LMBPoint;
            var RMBAttackResult = _defenceData.RMBPoint < offence.RMBPoint;
            var LLBAttackResult = _defenceData.LLBPoint < offence.LLBPoint;
            var RLBAttackResult = _defenceData.RLBPoint < offence.RLBPoint;
            
            var LHDamage = offence.LHPoint - _defenceData.LHPoint;
            var RHDamage = offence.RHPoint - _defenceData.RHPoint;
            var LUBDamage = offence.LUBPoint - _defenceData.LUBPoint;
            var RUBDamage = offence.RUBPoint - _defenceData.RUBPoint;
            var LMBDamage = offence.LMBPoint - _defenceData.LMBPoint;
            var RMBDamage = offence.RMBPoint - _defenceData.RMBPoint;
            var LLBDamage = offence.LLBPoint - _defenceData.LLBPoint;
            var RLBDamage = offence.RLBPoint - _defenceData.RLBPoint;
            
            _fightResult.Add(new OffenceResultData() {IsDamaged = LHAttackResult, OffencePoint = PointType.LH, Damage = LHDamage});
            _fightResult.Add(new OffenceResultData() {IsDamaged = RHAttackResult, OffencePoint = PointType.RH, Damage = RHDamage});
            _fightResult.Add(new OffenceResultData() {IsDamaged = LUBAttackResult, OffencePoint = PointType.LUB, Damage = LUBDamage});
            _fightResult.Add(new OffenceResultData() {IsDamaged = RUBAttackResult, OffencePoint = PointType.RUB, Damage = RUBDamage});
            _fightResult.Add(new OffenceResultData() {IsDamaged = LMBAttackResult, OffencePoint = PointType.LMB, Damage = LMBDamage});
            _fightResult.Add(new OffenceResultData() {IsDamaged = RMBAttackResult, OffencePoint = PointType.RMB, Damage = RMBDamage});
            _fightResult.Add(new OffenceResultData() {IsDamaged = LLBAttackResult, OffencePoint = PointType.LLB, Damage = LLBDamage});
            _fightResult.Add(new OffenceResultData() {IsDamaged = RLBAttackResult, OffencePoint = PointType.RLB, Damage = RLBDamage});

            return _fightResult;
        }

        public (int, int) CalculateDamage(List<OffenceResultData> offenceResults)
        {
            var oldValue = _hp;
            foreach (var result in offenceResults)
            {
                if (result.IsDamaged)
                {
                    DecreaseHP(result.Damage);
                }
            }

            var newValue = _hp;
            return (oldValue, newValue);
        }

        private void DecreaseHP(int damage)
        {
            _hp -= damage * _damageModifier;
        }
    }
}