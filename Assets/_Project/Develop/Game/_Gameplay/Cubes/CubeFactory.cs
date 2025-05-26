using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CubeFactory : PlaceholderFactory<CubeView, Cube>
    {
        private readonly DiContainer _container;
        private readonly CubeView _prefab;

        [Inject]
        public CubeFactory(DiContainer container, CubeView prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public Cube Create()
        {
            var view = Object.Instantiate(_prefab);
            var newCube = _container.Instantiate<Cube>(new object[] { view });

            return newCube;
        }
    }
}