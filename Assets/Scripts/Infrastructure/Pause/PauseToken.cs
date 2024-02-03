namespace Game.Infrastructure.Pause
{
    public class PauseToken
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
    }
}