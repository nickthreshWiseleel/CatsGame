using UnityEngine;

namespace Game.Infrastructure.Pause
{
    public class PausableWaitForSeconds : CustomYieldInstruction
    {
        private readonly PauseToken _pauseToken;
        private readonly float _seconds;
        private float _startTime;
        private float _cachedTime;

        public PausableWaitForSeconds(float seconds, PauseToken pauseToken)
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
                    return true;
                }
                else
                {
                    _cachedTime = Time.time - _startTime;
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