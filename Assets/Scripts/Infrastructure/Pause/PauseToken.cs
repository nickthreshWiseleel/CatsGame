namespace Game.Infrastructure.Pause
{
    public class PauseToken
    {
        private bool _isPaused;

        public bool IsPaused => _isPaused;

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause() // remove
        {
            _isPaused = false;
        }
    }
}