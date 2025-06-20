using Configs;
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

            var gameConfigs = _configsProvider.GameConfigs;
            var levelsConfigs = gameConfigs.LevelsConfigs;

            var levelConfigs = levelsConfigs
                .GetLevel(enterParams.Number)
                .As<TheoryLevelConfigs>();

            //UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            _ui.SetLevelTitle(levelConfigs);
            _ui.CreateProgressBar();

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}