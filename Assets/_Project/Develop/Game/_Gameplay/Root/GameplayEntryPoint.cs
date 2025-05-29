using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        private GameplayUI _ui;
        private CubeFactory _cubeFactory;

        [Inject]
        private void Construct(GameplayUI ui,
                               CubeFactory cubeFactory)
        {
            _ui = ui;
            _cubeFactory = cubeFactory;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            var cubesConfigs = _configsProvider.GameConfigs.CubesConfigs;

            _cubeFactory.Create(cubesConfigs.Cubes[0], new Vector3(-0.75f, 0, 0));
            _cubeFactory.Create(cubesConfigs.Cubes[1], new Vector3(0.75f, 0, 0));

            _uiRoot.AttachSceneUI(_ui);

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}