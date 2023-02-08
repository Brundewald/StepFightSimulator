using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FightSimulator
{
    public class FightSceneHandlerView : MonoBehaviour
    {
        private const string Even = "We call it  even!";
        private const string FirstFighterWin = "Figter 1 WIN!";
        private const string SecondFighterWin = "Figter 2 WIN!";
        
        [SerializeField] private FighterView _firstFighter;
        [SerializeField] private FighterView _secondFighter;
        [SerializeField] private Button _continueButton;
        [SerializeField] private WinnerMessageView winnerMessageView;
        private List<FighterData> _fighters;
        private List<FighterOffenceResultData> _fightResults;

        private void Awake()
        {
            _firstFighter.AnimationCompleted += CheckFightersHealth;
            _secondFighter.AnimationCompleted += ShowSecondAnimationStage;
            
        }

        private void OnDestroy()
        {
            _firstFighter.AnimationCompleted -= CheckFightersHealth;
            _secondFighter.AnimationCompleted -= ShowSecondAnimationStage;
        }

        public void InitiateFight(List<FighterData> fighters)
        {
            _continueButton.gameObject.SetActive(false);
            _fightResults = new List<FighterOffenceResultData>();
            _fighters = fighters;
            _firstFighter.SetHP(_fighters[0].HP);
            _secondFighter.SetHP(_fighters[1].HP);
            gameObject.SetActive(true);
            CalculateRound();
            ShowFirstAnimationStage();
        }

        private void CalculateRound()
        {
            for (var i = 0; i < _fighters.Count; i++)
            {
                if(i == 0)
                {
                    var offenceResults = _fighters[i].CheckAttackResult(_fighters[i + 1].Offence);
                    var hp = _fighters[i].CalculateDamage(offenceResults);
                    _fightResults.Add(new FighterOffenceResultData(){OffenceResults = offenceResults, ChangeHPValue = hp});
                }
                else
                {
                    var offenceResults =_fighters[i].CheckAttackResult(_fighters[i - 1].Offence);
                    var hp = _fighters[i].CalculateDamage(offenceResults);
                    _fightResults.Add(new FighterOffenceResultData(){OffenceResults = offenceResults, ChangeHPValue = hp});
                }
            }
        }

        private void CheckFightersHealth()
        {
            var firstFighterHaveHP = _fighters[0].HP > 0;
            var secondFighterHaveHP = _fighters[1].HP > 0;

            if (firstFighterHaveHP && secondFighterHaveHP)
            {
                _continueButton.gameObject.SetActive(true);;
            }
            else
            {
                if (!firstFighterHaveHP && !secondFighterHaveHP)
                {
                    SetEndGameMessage(Even);
                }
                else
                {
                    if (!firstFighterHaveHP)
                    {
                        SetEndGameMessage(SecondFighterWin);
                    }

                    if (!secondFighterHaveHP)
                    {
                        SetEndGameMessage(FirstFighterWin);
                    }
                }
            }
        }

        private void ShowFirstAnimationStage()
        {
            _secondFighter.RunAnimationSequence(_fightResults[1]);
        }
        private void ShowSecondAnimationStage()
        {
            _firstFighter.RunAnimationSequence(_fightResults[0]);
        }

        private void SetEndGameMessage(string message)
        {
            gameObject.SetActive(false);
            winnerMessageView.gameObject.SetActive(true);
            winnerMessageView.SetMessage(message);
        }
    }
}