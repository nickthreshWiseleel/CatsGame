using UnityEngine;
using Game.Infrastructure;

namespace Game
{
    public class Spawner
    {
        private struct Coordinates // of _spawnArea's corners
        {
            public float RightTopX;
            public float RightTopY;
            public float LeftBottomX;
            public float LeftBottomY;
        }

        private readonly Entity _entity;
        private readonly RectTransform _spawnArea;
        private readonly float _lifeDelay;

        private PrefabFactory<Entity> _factory;
        private Pool<Entity> _pool;

        private Coordinates _coordinates;

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
            GetCorners();
        }

        public Entity Spawn()
        {
            var entity = _pool.Get();
            entity.Init(_lifeDelay);
            SetPosition(entity);
            return entity;
        }

        public void Return(Entity entity)
        {
            _pool.Return(entity);
        }

        private void GetCorners()
        {
            var menuCorners = new Vector3[4];
            _spawnArea.GetWorldCorners(menuCorners);
            foreach (var corner in menuCorners)
            {
                if (corner.x > 0 && corner.y > 0)
                {
                    _coordinates.RightTopX = corner.x;
                    _coordinates.RightTopY = corner.y;
                }

                if (corner.x < 0 && corner.y < 0)
                {
                    _coordinates.LeftBottomX = corner.x;
                    _coordinates.LeftBottomY = corner.y;
                }
            }
        }

        private void SetPosition(Entity entity)
        {
            var horizontal = Random.Range(_coordinates.LeftBottomX, _coordinates.RightTopX);
            var vertical = Random.Range(_coordinates.LeftBottomY, _coordinates.RightTopY);

            entity.transform.position = new Vector2(horizontal, vertical);
        }
    }
}