using UnityEngine;

public class EntityFactory : IFactory<Entity>
{
    private Entity _entity;
    private EntityConfig _config;

    public EntityFactory(Entity entity, EntityConfig entityConfig)
    {
        _entity = entity;
        _config = entityConfig;
    }

    public Entity Create()
    {
        var created = Object.Instantiate(_entity);
        created.InitConfig(_config);
        created.name = _config.Name;
        created.GetComponent<SpriteRenderer>().sprite = _config.StartSprite;
        return created;
    }
}
