using System.Collections.Generic;

namespace Game.Infrastructure.Pause
{
    public class PauseManager : IPauseProvider
    {
        private readonly List<IPausable> _pausables;

        public PauseManager()
        {
            _pausables = new();
        }

        public PauseManager(IEnumerable<IPausable> pausables)
        {
            _pausables = new(pausables);
        }

        public void Add(IPausable pausable)
        {
            _pausables.Add(pausable);
        }

        public void Remove(IPausable pausable)
        {
            _pausables.Remove(pausable);
        }

        public void Pause()
        {
            foreach (IPausable pausable in _pausables)
            {
                pausable.Pause();
            }
        }

        public void Unpause()
        {
            foreach (IPausable pausable in _pausables)
            {
                pausable.Unpause();
            }
        }
    }
}