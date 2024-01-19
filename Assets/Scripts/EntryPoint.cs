using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ClickHandler _clickHandler;
    private VFXPlayer _VFXPlayer = new();
    private SoundPlayer _soundPlayer = new();
    private List<IPausable> _pauseList = new();
    private PauseManager _pauseManager;

    public ClickHandler ClickHandler => _clickHandler;
    public VFXPlayer VFXPlayer => _VFXPlayer;
    public SoundPlayer SoundPlayer => _soundPlayer;
    public PauseManager PauseManager => _pauseManager;

    private void Awake()
    {
        _pauseManager = new PauseManager(_pauseList);
    }
}
