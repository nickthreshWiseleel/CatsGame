using UnityEngine;


namespace Game
{
    public class VFXPlayer
    {
        private readonly ExplosionVFX _explosion;
        private ExplosionVFXFactory _explosionFactory;
        private Pool<ExplosionVFX> _pool;
        private PauseManager _pauseManager;

        public VFXPlayer(ExplosionVFX explosion, PauseManager pauseManager)
        {
            _explosion = explosion;
            _pauseManager = pauseManager;
        }

        public void Init()
        {
            _explosionFactory = new(_explosion);
            _pool = new(_explosionFactory);
        }

        public void Play(Vector2 position)
        {
            var explosion = _pool.Get(_explosion);

            _pauseManager.Add(explosion);

            explosion.OnAnimationEnded(explosion =>
            {
                Return(explosion);
                _pauseManager.Remove(explosion);
            });

            explosion.transform.position = position;
            explosion.Play();
        }

        public void Return(ExplosionVFX explosion)
        {
            _pool.Return(explosion);
        }
    }
}