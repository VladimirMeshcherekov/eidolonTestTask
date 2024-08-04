using System;
using Cysharp.Threading.Tasks;

namespace Utilities
{
    public class AsyncTimer
    {
        private readonly Action _action;
        private readonly int _tickDelayInMilliseconds;
        private bool _autoRepeat;
        public bool IsTimerTicking { get; private set; }

        public AsyncTimer(Action action, int tickDelayInMilliseconds, bool autoRepeat)
        {
            _action = action;
            _tickDelayInMilliseconds = tickDelayInMilliseconds;
            _autoRepeat = autoRepeat;
        }

        public void StartTimer()
        {
            IsTimerTicking = true;
            TimerTick();
        }

        private async void TimerTick()
        {
            await UniTask.Delay(_tickDelayInMilliseconds);
            if (IsTimerTicking == false)
            {
                return;
            }

            _action?.Invoke();
            if (_autoRepeat == false)
            {
                return;
            }

            TimerTick();
        }

        public void StopTimer()
        {
            IsTimerTicking = false;
        }
    }
}