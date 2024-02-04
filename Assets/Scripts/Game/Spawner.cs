using UnityEngine;
using Game.Infrastructure;

namespace Game
{
    public class Spawner
    {
        private struct Corners
        {
            public Vector3 RightTop;
            public Vector3 LeftBottom;
        }

        private readonly Entity _entity;
        private readonly RectTransform _spawnArea;
        private readonly float _lifeDelay;

        private PrefabFactory<Entity> _factory;
        private Pool<Entity> _pool;

        private Corners _corners;

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
            _corners = new();
        }

        public Entity Spawn()
        {
            var entity = _pool.Get();
            entity.Init(_lifeDelay);
            GetCorners();
            SetPosition(entity);
            return entity;
        }

        public void Return(Entity entity)
        {
            _pool.Return(entity);
        }

        private void GetCorners()
        {
            var corners = new Vector3[4];
            _spawnArea.GetWorldCorners(corners);
            foreach (var corner in corners)
            {
                if (corner.x > 0 && corner.y > 0)
                {
                    _corners.RightTop.x = corner.x;
                    _corners.RightTop.y = corner.y;
                }

                if (corner.x < 0 && corner.y < 0)
                {
                    _corners.LeftBottom.x = corner.x;
                    _corners.LeftBottom.y = corner.y;
                }
            }
        }

        private void SetPosition(Entity entity)
        {
            var horizontal = Random.Range(_corners.LeftBottom.x, _corners.RightTop.x);
            var vertical = Random.Range(_corners.LeftBottom.y, _corners.RightTop.y);

            entity.transform.position = new Vector2(horizontal, vertical);
        }
    }
}