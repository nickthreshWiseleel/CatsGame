using UnityEngine;
using Infrastructure;

namespace Game
{
    public class Spawner
    {
        private struct Coordinates
        {
            public float _highestCornerX;
            public float _highestCornerY;
            public float _lowestCornerX;
            public float _lowestCornerY;
        }

        private readonly Entity _entity;
        private PrefabFactory<Entity> _factory;
        private Pool<Entity> _pool;

        private readonly RectTransform _spawnArea;
        private Coordinates _coordinates;

        private readonly float _lifeDelay;

        public Spawner(Entity entity, RectTransform spawnArea, float lifeDelay)
        {
            _entity = entity;
            _spawnArea = spawnArea;
            _lifeDelay = lifeDelay;
        }

        public void Init()
        {
            _factory = new(_entity);
            _pool = new(_factory);
            _coordinates = new();
        }

        public Entity Spawn()
        {
            var entity = _pool.Get(_entity);
            entity.Init(_lifeDelay);
            SetPosition(entity);
            return entity;
        }

        public void Return(Entity entity)
        {
            _pool.Return(entity);
        }

        private void SetPosition(Entity entity)
        {
            var menuCorners = new Vector3[4];
            _spawnArea.GetWorldCorners(menuCorners);
            foreach (var corner in menuCorners)
            {
                if (corner.x > 0 && corner.y > 0)
                {
                    _coordinates._highestCornerX = corner.x;
                    _coordinates._highestCornerY = corner.y;
                }

                if (corner.x < 0 && corner.y < 0)
                {
                    _coordinates._lowestCornerX = corner.x;
                    _coordinates._lowestCornerY = corner.y;
                }
            }

            var horizontal = Random.Range(_coordinates._lowestCornerX, _coordinates._highestCornerX);
            var vertical = Random.Range(_coordinates._lowestCornerY, _coordinates._highestCornerY);

            entity.transform.position = new Vector2(horizontal, vertical);
        }
    }
}