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
            yield return Run(enterParams.As<LevelMenuEnterParams>());
        }

        private IEnumerator Run(LevelMenuEnterParams enterParams)
        {
            var isLoaded = false;

            //UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            _ui.CreateButtons();
            _ui.ScrollToCurrentButton(true);

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}