using UnityEngine;


namespace Game
{
    public class ExplosionVFXFactory : IFactory<ExplosionVFX>
    {
        private ExplosionVFX _explosion;

        public ExplosionVFXFactory(ExplosionVFX explosion)
        {
            _explosion = explosion;
        }

        public ExplosionVFX Create()
        {
            var created = Object.Instantiate(_explosion);
            created.name = _explosion.gameObject.name;
            return created;
        }
    }
}