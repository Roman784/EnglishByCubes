using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace LevelMenu
{
    public class LevelMenuEntryPoint : SceneEntryPoint
    {
        private LevelMenuUI _ui;

        [Inject]
        private void Construct(LevelMenuUI ui)
        {
            _ui = ui;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}