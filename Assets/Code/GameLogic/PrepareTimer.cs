using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FightSimulator
{
    internal class PrepareTimer
    {
        private float _time;
        private CancellationTokenSource _cts;

        private float CurrentTime
        {
            get => _time;
            set
            {
                if (value != _time)
                {
                    _time = value;
                    OnTimeChange.Invoke(_time);
                }
            }
        }
        
        public event Action TimerElapsed = delegate {  };
        public Action<float> OnTimeChange = delegate {  };        
        public void StartTimer(float time)
        {
            _time = time;
            _cts = new CancellationTokenSource();
            RunTimer().Forget();
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        private async UniTaskVoid RunTimer()
        {
            while (!_cts.IsCancellationRequested)
            {
                CurrentTime = _time - Time.deltaTime;
                if (_time <= 0)
                {
                    TimerElapsed.Invoke(); 
                    break;
                }
                await UniTask.Yield();
            }
        }
    }
}