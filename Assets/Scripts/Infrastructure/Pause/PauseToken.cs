using System;
using UnityEngine;

namespace Pause
{
    public class PauseToken : IDisposable
    {
        private bool _isPaused;

        public bool IsPaused => _isPaused;

        public bool Pause()
        {
            return _isPaused = true;
        }

        public bool Unpause()
        {
            return _isPaused = false;
        }

        public void Dispose()
        {
        }
    }
}