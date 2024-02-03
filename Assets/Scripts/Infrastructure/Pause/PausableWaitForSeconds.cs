using UnityEngine;

namespace Game.Infrastructure.Pause
{
    public class PausableWaitForSeconds : CustomYieldInstruction
    {
        private readonly PauseToken _pauseToken;
        private float _seconds;
        private float _startTime;
        private float _cachedTime;

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
                if (_pauseToken.IsPaused)
                {
                    _startTime = Time.time - _cachedTime;
                }
                else
                {
                    _cachedTime = Time.time - _startTime;
                }

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
    }
}