using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace LevelMenu
{
    public class LevelMenuEntryPoint : SceneEntryPoint
    {
        [SerializeField] private LevelButton _levelButtonPrefab;

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

            if (!_gameStateProvider.GameStateProxy.State.IsFirstEntrance)
            {
                _gameStateProvider.GameStateProxy.SetIsFirstEntrance(true);
                _rootPopUpsProvider.OpenFirstEntrance();
            }

            if (_sceneProvider.PreviousSceneParams.SceneName == Scenes.GAMEPLAY ||
                _sceneProvider.PreviousSceneParams.SceneName == Scenes.MISTAKE_CORRECTION ||
                _sceneProvider.PreviousSceneParams.SceneName == Scenes.TEMPLATE)
            {
                if (_configsProvider.GameConfigs.LevelsConfigs.IsLastLevel(
                    _gameStateProvider.GameStateProxy.State.LastCompletedLevelNumber))
                {
                    _rootPopUpsProvider.OpenGameCompletedPopUp();
                }
            }

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}