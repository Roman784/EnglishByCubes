using Configs;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Gameplay
{
    public class CubeFactory : PlaceholderFactory<CubeView, Cube>
    {
        private readonly DiContainer _container;
        private readonly CubeView _prefab;
        private readonly GameFieldService _gameFieldService;
        private readonly CubesPositionPreviewService _cubesPositionPreviewService;

        [Inject]
        public CubeFactory(DiContainer container, CubeView prefab,
                           GameFieldService gameFieldService, CubesPositionPreviewService cubesPositionPreviewService)
        {
            _container = container;
            _prefab = prefab;
            _gameFieldService = gameFieldService;
            _cubesPositionPreviewService = cubesPositionPreviewService;
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
            var newCube = _container.Instantiate<Cube>(new object[] { view, configs, _gameFieldService, _cubesPositionPreviewService });

            return newCube;
        }
    }
}