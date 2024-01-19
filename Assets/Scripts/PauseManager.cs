using System.Collections.Generic;

public class PauseManager : IPausable
{
    private List<IPausable> _pausables = new List<IPausable>();

    public PauseManager(List<IPausable> pausables)
    {
        _pausables = pausables;
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
