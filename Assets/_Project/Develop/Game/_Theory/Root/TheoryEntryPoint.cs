using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Theory
{
    public class TheoryEntryPoint : SceneEntryPoint
    {
        private TheoryUI _ui;

        [Inject]
        private void Construct(TheoryUI ui)
        {
            _ui = ui;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<TheoryEnterParams>());
        }

        private IEnumerator Run(TheoryEnterParams enterParams)
        {
            var isLoaded = false;

            //UI.
            _uiRoot.AttachSceneUI(_ui);

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}