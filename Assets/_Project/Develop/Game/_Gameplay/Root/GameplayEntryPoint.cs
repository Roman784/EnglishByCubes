using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Vector3 _slotBarPosition;

        private GameplayUI _ui;
        private CubeFactory _cubeFactory;
        private SlotBarFactory _slotBarFactory;

        [Inject]
        private void Construct(GameplayUI ui,
                               CubeFactory cubeFactory,
                               SlotBarFactory slotBarFactory)
        {
            _ui = ui;
            _slotBarFactory = slotBarFactory;
            _cubeFactory = cubeFactory;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            var cubesConfigs = _configsProvider.GameConfigs.CubesConfigs;

            _cubeFactory.Create(cubesConfigs.Cubes[0], new Vector3(-0.75f, 0, 0));
            _cubeFactory.Create(cubesConfigs.Cubes[1], new Vector3(0.75f, 0, 0));

            // Slot bar.
            var slotBar = _slotBarFactory.Create(_slotBarPosition);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}