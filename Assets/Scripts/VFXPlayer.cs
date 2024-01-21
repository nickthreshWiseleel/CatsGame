using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : IPausable
{
    // ExplosionFactory<Explosion>
    // Pool<Explosion>

    public VFXPlayer()
    {
    }

    public void Play(IVFX VFX)
    {
        VFX.Play();
    }


    public void Pause()
    {
        Debug.Log("Paused");
    }

    public void Unpause()
    {
        Debug.Log("Unpaused");
    }
}