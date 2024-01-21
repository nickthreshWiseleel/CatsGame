using System.Collections.Generic;

public class PauseManager : IPausable
{
    private List<IPausable> _pausables = new List<IPausable>();

    public PauseManager()
    {
        
    }

    public PauseManager(List<IPausable> pausables)
    {
        _pausables.AddRange(pausables);
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
        foreach (IPausable p in _pausables)
        {
            p.Pause();
        }
    }

    public void Unpause()
    {
        foreach (IPausable p in _pausables)
        {
            p.Unpause();
        }
    }
}
