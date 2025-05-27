using GameRoot;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        private CubeFactory _cubeFactory;

        [Inject]
        private void Construct(CubeFactory cubeFactory)
        {
            _cubeFactory = cubeFactory;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            var cubesConfigs = _configsProvider.GameConfigs.CubesConfigs;

            _cubeFactory.Create(cubesConfigs.Cubes[0], new Vector3(-0.75f, 0, 0));
            _cubeFactory.Create(cubesConfigs.Cubes[1], new Vector3(0.75f, 0, 0));

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}