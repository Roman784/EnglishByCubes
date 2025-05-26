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

            _cubeFactory.Create();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}