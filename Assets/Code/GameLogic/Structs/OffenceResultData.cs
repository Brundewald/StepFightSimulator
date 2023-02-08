using System.Collections.Generic;

namespace FightSimulator
{
    public struct OffenceResultData
    {
        public bool IsDamaged;
        public PointType OffencePoint;
        public int Damage;
    }

    public struct FighterOffenceResultData
    {
        public List<OffenceResultData> OffenceResults;
        public (int, int) ChangeHPValue;
    }
}