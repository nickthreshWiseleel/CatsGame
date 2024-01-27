using UnityEngine;

namespace Game
{
    public class EntitySoundFactory : IFactory<EntitySound>
    {
        private EntitySound _sound;

        public EntitySoundFactory(EntitySound sound)
        {
            _sound = sound;
        }

        public EntitySound Create()
        {
            var created = Object.Instantiate(_sound);
            created.name = _sound.gameObject.name;
            return created;
        }
    }
}