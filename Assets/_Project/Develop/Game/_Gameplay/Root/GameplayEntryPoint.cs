using GameRoot;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        [Inject]
        private void Construct()
        {
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}