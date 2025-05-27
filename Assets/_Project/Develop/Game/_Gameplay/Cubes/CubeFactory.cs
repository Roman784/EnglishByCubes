using Configs;
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

        public Cube Create(CubeConfigs configs, Vector3 position)
        {
            var newCube = Create(configs);
            newCube.SetPosition(position);

            return newCube;
        }

        public Cube Create(CubeConfigs configs)
        {
            var view = Object.Instantiate(_prefab);
            var newCube = _container.Instantiate<Cube>(new object[] { view, configs });

            return newCube;
        }
    }
}