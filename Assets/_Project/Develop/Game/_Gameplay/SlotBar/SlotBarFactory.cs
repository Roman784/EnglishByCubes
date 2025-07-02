using Audio;
using Configs;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class SlotBarFactory : PlaceholderFactory<SlotBarView, SlotBar>
    {
        private readonly DiContainer _container;
        private readonly SlotBarView _prefab;
        private readonly CubeFactory _cubeFactory;
        private readonly AudioProvider _audioProvider;
        private readonly IConfigsProvider _configsProvider;

        [Inject]
        public SlotBarFactory(DiContainer container, SlotBarView prefab, CubeFactory cubeFactory, 
                              AudioProvider audioProvider, IConfigsProvider configsProvider)
        {
            _container = container;
            _prefab = prefab;
            _cubeFactory = cubeFactory;
            _audioProvider = audioProvider;
            _configsProvider = configsProvider;
        }

        public SlotBar Create(Vector3 position)
        {
            var newSlotBar = Create();
            newSlotBar.SetPosition(position);

            return newSlotBar;
        }

        public SlotBar Create()
        {
            var view = Object.Instantiate(_prefab);
            var newSlotBar = _container.Instantiate<SlotBar>(new object[] { view, _cubeFactory, _audioProvider, _configsProvider });

            return newSlotBar;
        }
    }
}