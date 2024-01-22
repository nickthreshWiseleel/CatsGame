using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : IPausable
{
    private Explosion _explosion;
    private ExplosionFactory _explosionFactory;
    private Pool<Explosion> _pool;

    public VFXPlayer(Explosion explosion)
    {
        _explosion = explosion;
        _explosionFactory = new(_explosion);
        _pool = new(_explosionFactory);
    }

    public void Empty()
    {
        Debug.Log("Empty");
    }

    public void Play(Vector2 position)
    {
        var explosion = _pool.Get(_explosion);

        explosion.OnAnimationEnded(explosion =>
        {
            Return(explosion);
        });

        explosion.transform.position = position;
        explosion.Play();
    }

    public void Return(Explosion explosion)
    {
        _pool.Return(explosion);
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