using Configs;
using Gameplay;
using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace MistakeCorrection
{
    public class MistakeCorrectionEntryPoint : SceneEntryPoint
    {
        private MistakeCorrectionUI _ui;
        private MistakeCorrectionFieldService _gameFieldService;
        private MistakeCorrectionLevelPassingService _levelPassingService;

        [Inject]
        private void Construct(MistakeCorrectionUI ui, 
                               IGameFieldService gameFieldService, ILevelPassingService levelPassingService)
        {
            _ui = ui;
            _gameFieldService = (MistakeCorrectionFieldService)gameFieldService;
            _levelPassingService = (MistakeCorrectionLevelPassingService)levelPassingService;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<MistakeCorrectionEnterParams>());
        }

        private IEnumerator Run(MistakeCorrectionEnterParams enterParams)
        {
            var isLoaded = false;

            var gameConfigs = _configsProvider.GameConfigs;
            var cubesConfigs = gameConfigs.CubesConfigs;
            var levelsConfigs = gameConfigs.LevelsConfigs;

            var levelConfigs = levelsConfigs
                .GetLevel(enterParams.Number)
                .As<MistakeCorrectionConfigs>();

            // Cubes.
            CreateCubes(levelConfigs);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams, levelConfigs.Sentences.Count);
            _ui.SetLevelTitle(levelConfigs);

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }

        private void CreateCubes(MistakeCorrectionConfigs configs)
        {
            foreach (var sentence in configs.Sentences)
            {

            }
        }
    }
}