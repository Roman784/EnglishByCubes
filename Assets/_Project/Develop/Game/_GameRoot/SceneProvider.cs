using Collection;
using Configs;
using Gameplay;
using LevelMenu;
using Template;
using Theory;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class SceneProvider
    { 
        private IConfigsProvider _configsProvider;
        private SceneEnterParams _previousSceneParams;
        private SceneLoader _sceneLoader;

        private LevelsConfigs LevelsConfigs => _configsProvider.GameConfigs.LevelsConfigs;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, UIRoot uiRoot)
        {
            _configsProvider = configsProvider;
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
            if (!LevelsConfigs.IsLevelExist(theoryNumber, LevelMode.Theory))
            {
                OpenLevelMenu(currentSceneParams);
                return;
            }

            _previousSceneParams = currentSceneParams;

            var enterParams = new TheoryEnterParams(theoryNumber);
            _sceneLoader.LoadAndRunTheory(enterParams);
        }

        public void OpenTemplate(SceneEnterParams currentSceneParams, int templateNumber)
        {
            if (!LevelsConfigs.IsLevelExist(templateNumber, LevelMode.Template))
            {
                OpenLevelMenu(currentSceneParams);
                return;
            }

            _previousSceneParams = currentSceneParams;

            var enterParams = new TemplateEnterParams(templateNumber);
            _sceneLoader.LoadAndRunTemplate(enterParams);
        }

        public void OpenPractice(SceneEnterParams currentSceneParams, int practiceNumber)
        {
            if (!LevelsConfigs.IsLevelExist(practiceNumber, LevelMode.Practice))
            {
                OpenLevelMenu(currentSceneParams);
                return;
            }

            _previousSceneParams = currentSceneParams;

            var enterParams = new GameplayEnterParams(practiceNumber);
            _sceneLoader.LoadAndRunGameplay(enterParams);
        }

        public void OpenCollection(SceneEnterParams currentSceneParams)
        {
            _previousSceneParams = currentSceneParams;

            var enterParams = new CollectionEnterParams();
            _sceneLoader.LoadAndRunCollection(enterParams);
        }

        public void OpenPreviousScene(SceneEnterParams currentSceneParams)
        {
            switch (_previousSceneParams.SceneName)
            {
                case Scenes.LEVEL_MENU:
                    OpenLevelMenu(_previousSceneParams);
                    break;
                case Scenes.THEORY:
                    OpenTheory(_previousSceneParams, _previousSceneParams.As<TheoryEnterParams>().Number);
                    break;
                case Scenes.GAMEPLAY:
                    OpenPractice(_previousSceneParams, _previousSceneParams.As<GameplayEnterParams>().Number);
                    break;
                case Scenes.COLLECTION:
                    OpenCollection(_previousSceneParams);
                    break;
                case Scenes.TEMPLATE:
                    OpenTemplate(_previousSceneParams, _previousSceneParams.As<TemplateEnterParams>().Number);
                    break;
            }

            _previousSceneParams = currentSceneParams;
        }
    }
}