using UnityEngine;

namespace Pause
{
    public class PausableWaitForSeconds : CustomYieldInstruction
    {
        private float _seconds;
        private float _startTime;
        private float _cachedTime;
        private readonly PauseToken _pauseToken;

        public PausableWaitForSeconds(float seconds, ref PauseToken pauseToken)
        {
            _startTime = Time.time;
            _seconds = seconds;
            _pauseToken = pauseToken;
        }

        public override bool keepWaiting
        {
            get
            {
                CacheTime();

                if (_pauseToken.IsPaused)
                {
                    return true;
                }

                if (Time.time - _startTime < _seconds)
                {
                    return true;
                }

                return false;
            }
        }

        public void CacheTime()
        {
            if(_pauseToken.IsPaused == false)
            {
                _cachedTime = Time.time - _startTime;
            }
            else
            {
                _startTime = Time.time - _cachedTime;
            }
        }
    }
}