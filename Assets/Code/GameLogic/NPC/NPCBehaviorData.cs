using System.Collections.Generic;
using UnityEngine;

namespace FightSimulator.NPC
{
    [CreateAssetMenu(menuName = "NPCBehavior", fileName = "NPCBehavior")]
    public class NPCBehaviorData : ScriptableObject
    {
        [SerializeField] private List<StatPreset> _offenceStats;
        [SerializeField] private List<StatPreset> _defenceStats;

        public StatSetup GetOffenceStat()
        {
            var randomIndex = Random.Range(0, _offenceStats.Count);
            var statSetup = new StatSetup(StatSetType.OFFENCE);
            statSetup.SetupNPC(_offenceStats[randomIndex]);
            return statSetup;
        }
        public StatSetup GetDefenceStat()
        {
            var randomIndex = Random.Range(0, _offenceStats.Count);
            var statSetup = new StatSetup(StatSetType.OFFENCE);
            statSetup.SetupNPC(_defenceStats[randomIndex]);
            return statSetup;
        } 
    }
}