using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FightSimulator
{
    public class FighterView : MonoBehaviour
    {
        [Header("HP field")] 
        [SerializeField] private TextMeshProUGUI _hpField;

        [Header("Animation settings")]
        [SerializeField] private Color _defendColor = Color.green;
        [SerializeField] private Color _hitColor = Color.red;
        [SerializeField] private float _animationDuration;


        [Header("Stat buttons")] 
        [SerializeField] private Image _LHImage;
        [SerializeField] private Image _RHImage;
        [SerializeField] private Image _LUBImage;
        [SerializeField] private Image _RUBImage;
        [SerializeField] private Image _LMBImage;
        [SerializeField] private Image _RMBImage;
        [SerializeField] private Image _LLBImage;
        [SerializeField] private Image _RLBImage;

        public event Action AnimationCompleted = delegate {  };
        
        public void RunAnimationSequence(FighterOffenceResultData result)
        {
            var newSequence = DOTween.Sequence();
            foreach (var offencePoint in result.OffenceResults)
            {
                if(offencePoint.Damage > 1)
                {
                    for (var i = 0; i < offencePoint.Damage; i++)
                    {
                        var newColorTween = StartPointAttackAnimation(offencePoint.IsDamaged, false, offencePoint.OffencePoint);
                        newSequence.Append(newColorTween);
                        var newTween = StartPointAttackAnimation(offencePoint.IsDamaged, true, offencePoint.OffencePoint);
                        newSequence.Append(newTween);
                    }
                }
                else
                {
                    var newTween = StartPointAttackAnimation(offencePoint.IsDamaged, false, offencePoint.OffencePoint);
                    newSequence.Append(newTween);
                }
            }

            newSequence.AppendCallback(() => RunHPAnimation(result.ChangeHPValue).Forget());
        }

        private void OnEnable()
        {
            ResetColor();
        }

        private void ResetColor()
        {
            _LHImage.color = Color.white;
            _RHImage.color = Color.white;
            _LUBImage.color = Color.white;
            _RUBImage.color = Color.white;
            _LMBImage.color = Color.white;
            _RMBImage.color = Color.white;
            _LLBImage.color = Color.white;
            _RLBImage.color = Color.white;
        }

        private async UniTask RunHPAnimation((int, int) changeHpValue)
        {
            var oldValue = changeHpValue.Item1;
            var newValue = changeHpValue.Item2;
            
            for (var i = oldValue; i >= newValue; i--)
            {
                SetHP(i);
                await UniTask.Delay(200);
            }
            
            AnimationCompleted.Invoke();
        }

        private Tween StartPointAttackAnimation(bool isDamaged, bool isMultipleDamage, PointType point)
        {
            switch (point)
            {
                case PointType.LH:
                    return isMultipleDamage ? ScaleAnimation(_LHImage) : ColorAnimation(_LHImage ,isDamaged);
                case PointType.RH:
                    return isMultipleDamage ? ScaleAnimation(_RHImage) : ColorAnimation(_RHImage ,isDamaged);
                case PointType.LUB:
                    return isMultipleDamage ? ScaleAnimation(_LUBImage) : ColorAnimation(_LUBImage ,isDamaged);
                case PointType.RUB:
                    return isMultipleDamage ? ScaleAnimation(_RUBImage) : ColorAnimation(_RUBImage ,isDamaged);
                case PointType.LMB:
                    return isMultipleDamage ? ScaleAnimation(_LMBImage) : ColorAnimation(_LMBImage ,isDamaged);
                case PointType.RMB:
                    return isMultipleDamage ? ScaleAnimation(_RMBImage) : ColorAnimation(_RMBImage ,isDamaged);
                case PointType.LLB:
                    return isMultipleDamage ? ScaleAnimation(_LLBImage) : ColorAnimation(_LLBImage ,isDamaged);
                case PointType.RLB:
                    return isMultipleDamage ? ScaleAnimation(_RLBImage) : ColorAnimation(_RLBImage ,isDamaged);
            }

            return null;
        }

        private Tween ScaleAnimation(Image target)
        {
            return target.transform.DOPunchScale(Vector3.one * 1.1f, _animationDuration, 5).SetEase(Ease.InBounce);
        }

        private TweenerCore<Color, Color, ColorOptions> ColorAnimation(Image target, bool isDamaged)
        {
            return target.DOColor(isDamaged ? _hitColor : _defendColor, _animationDuration);
        }

        public void SetHP(int hp)
        {
            var value = hp > 0 ? hp : 0;
            _hpField.text = $"HP: {value}";
        }
    }
}