using Configs;
using GameRoot;
using Pause;
using Theme;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class CubeFactory : PlaceholderFactory<CubeView, Cube>
    {
        private readonly DiContainer _container;
        private readonly CubeView _prefab;
        private readonly IGameFieldService _gameFieldService;
        private readonly CubesPositionPreviewService _cubesPositionPreviewService;
        private readonly ThemeProvider _themeProvider;
        private readonly PauseProvider _pauseProvider;

        [Inject]
        public CubeFactory(DiContainer container, CubeView prefab,
                           IGameFieldService gameFieldService, CubesPositionPreviewService cubesPositionPreviewService,
                           ThemeProvider themeProvider, PauseProvider pauseProvider)
        {
            _container = container;
            _prefab = prefab;
            _gameFieldService = gameFieldService;
            _cubesPositionPreviewService = cubesPositionPreviewService;
            _themeProvider = themeProvider;
            _pauseProvider = pauseProvider;
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

            _themeProvider.Customize(view.gameObject);

            _pauseProvider.Add(view);
            view.OnDestroy.AddListener(() => _pauseProvider.Remove(view));

            return newCube;
        }
    }
}