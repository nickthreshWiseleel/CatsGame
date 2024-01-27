using UnityEngine;

namespace Game
{
    public class EntityFactory : IFactory<Entity>
    {
        private readonly Entity _entity;

        public EntityFactory(Entity entity)
        {
            _entity = entity;
        }

        public Entity Create()
        {
            var created = Object.Instantiate(_entity);
            created.name = _entity.gameObject.name;
            return created;
        }
    }
}