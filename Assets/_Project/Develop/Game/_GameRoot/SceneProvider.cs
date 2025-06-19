using Gameplay;
using LevelMenu;
using Theory;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class SceneProvider
    {
        private SceneEnterParams _previousSceneParams;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(UIRoot uiRoot)
        {
            _sceneLoader = new(uiRoot);
        }

        public void OpenLevelMenu(SceneEnterParams currentSceneParams)
        {
            _previousSceneParams = currentSceneParams;

            var enterParams = new LevelMenuEnterParams();
            _sceneLoader.LoadAndRunLevelMenu(enterParams);
        }

        public void OpenTheory(SceneEnterParams currentSceneParams, int theoryNumber)
        {
            _previousSceneParams = currentSceneParams;

            var enterParams = new TheoryEnterParams(theoryNumber);
            _sceneLoader.LoadAndRunTheory(enterParams);
        }

        public void OpenTemplates(SceneEnterParams currentSceneParams)
        {
            _previousSceneParams = currentSceneParams;
        }

        public void OpenPractice(SceneEnterParams currentSceneParams, int practiceNumber)
        {
            _previousSceneParams = currentSceneParams;

            var enterParams = new GameplayEnterParams(practiceNumber);
            _sceneLoader.LoadAndRunGameplay(enterParams);
        }

        public void OpenPreviousScene(SceneEnterParams currentSceneParams)
        {
            switch (_previousSceneParams.SceneName)
            {
                case Scenes.LEVEL_MENU:
                    _sceneLoader.LoadAndRunLevelMenu(_previousSceneParams.As<LevelMenuEnterParams>());
                    break;
                case Scenes.THEORY:
                    _sceneLoader.LoadAndRunTheory(_previousSceneParams.As<TheoryEnterParams>());
                    break;
                case Scenes.GAMEPLAY:
                    _sceneLoader.LoadAndRunGameplay(_previousSceneParams.As<GameplayEnterParams>());
                    break;
            }

            _previousSceneParams = currentSceneParams;
        }
    }
}