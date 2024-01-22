using UnityEngine;

public class Spawner
{
    private RectTransform _spawnArea;
    private Entity _entity;
    private EntityFactory _factory;
    private Pool<Entity> _pool;

    private float _highestCornerX;
    private float _highestCornerY;
    private float _lowestCornerX;
    private float _lowestCornerY;

    public Spawner(Entity entity, RectTransform spawnArea, float lifetime)
    {
        _entity = entity;
        _spawnArea = spawnArea;
        _factory = new(_entity, lifetime);
        _pool = new(_factory);
    }

    public Entity Spawn()
    {
        _entity = _pool.Get(_entity);
        SetPosition();
        return _entity;
    }

    public void Return(Entity entity)
    {
        _pool.Return(entity);
    }

    private void SetPosition()
    {
        var menuCorners = new Vector3[4];
        _spawnArea.GetWorldCorners(menuCorners);
        foreach (var corner in menuCorners)
        {
            if (corner.x > 0 && corner.y > 0)
            {
                _highestCornerX = corner.x;
                _highestCornerY = corner.y;
            }

            if (corner.x < 0 && corner.y < 0)
            {
                _lowestCornerX = corner.x;
                _lowestCornerY = corner.y;
            }
        }

        var horizontal = Random.Range(_lowestCornerX, _highestCornerX);
        var vertical = Random.Range(_lowestCornerY, _highestCornerY);

        _entity.transform.position = new Vector2(horizontal, vertical);
    }
}
