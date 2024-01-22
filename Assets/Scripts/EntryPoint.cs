using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ClickHandler _clickHandler;
    [SerializeField] private Lifetime _lifetime;
    private VFXPlayer _VFXPlayer;
    private SoundPlayer _soundPlayer = new();
    private PauseManager _pauseManager = new();

    private void Awake()
    {
        //_lifetime.Init(_VFXPlayer, _soundPlayer);
    }
}
