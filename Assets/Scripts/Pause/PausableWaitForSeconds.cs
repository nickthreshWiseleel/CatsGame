using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class PausableWaitForSeconds : CustomYieldInstruction
    {
        private float _seconds;
        private float _startTime;
        private ICoroutinePausable _pausable;

        public override bool keepWaiting
        {
            get
            {
                if (Time.time - _startTime < _seconds)
                {
                    return true;
                }
                return false;
            }
        }

        public async void OnPaused()
        {
            float cachedTime = Time.time - _startTime;
            while (_pausable.IsPaused == true)
            {
                await Task.Delay(1);
                _startTime = Time.time - cachedTime;
            }
        }

        public PausableWaitForSeconds(float seconds, ICoroutinePausable pausable)
        {
            _startTime = Time.time;
            _seconds = seconds;
            _pausable = pausable;
            _pausable.Paused += OnPaused;
        }
    }
}