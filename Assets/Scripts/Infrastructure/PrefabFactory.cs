using UnityEngine;

namespace Game.Infrastructure
{
    public class PrefabFactory<T> : IFactory<T> where T : Component
    {
        private readonly T _prefab;

        public PrefabFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}