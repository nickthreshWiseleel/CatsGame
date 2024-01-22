using UnityEngine;

public class EntityFactory : IFactory<Entity>
{
    private Entity _entity;
    private float _lifetime;

    public EntityFactory(Entity entity, float lifetime)
    {
        _entity = entity;
        _lifetime = lifetime;
    }

    public Entity Create()
    {
        var created = Object.Instantiate(_entity);
        created.name = _entity.gameObject.name;
        created.Init(_lifetime);
        return created;
    }
}
