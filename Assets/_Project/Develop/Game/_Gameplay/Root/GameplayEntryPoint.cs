using Configs;
using DG.Tweening;
using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Vector3 _slotBarPosition;

        private GameplayUI _ui;
        private SlotBarFactory _slotBarFactory;
        private TaskPassingService _taskPassingService;
        private GameplayPopUpProvider _gameplayPopUpProvider;

        private bool _isLevelCompleted;

        [Inject]
        private void Construct(GameplayUI ui,
                               SlotBarFactory slotBarFactory,
                               TaskPassingService taskPassingService,
                               GameplayPopUpProvider gameplayPopUpProvider)
        {
            _ui = ui;
            _slotBarFactory = slotBarFactory;
            _taskPassingService = taskPassingService;
            _gameplayPopUpProvider = gameplayPopUpProvider;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<GameplayEnterParams>());
        }

        private IEnumerator Run(GameplayEnterParams enterParams)
        {
            var isLoaded = false;

            var gameConfigs = _configsProvider.GameConfigs;
            var cubesConfigs = gameConfigs.CubesConfigs;
            var levelsConfigs = gameConfigs.LevelsConfigs;

            if (!levelsConfigs.IsLevelExist(enterParams.Number))
            {
                _sceneProvider.OpenLevelMenu(enterParams);
                yield break;
            }

            var taskConfigs = levelsConfigs
                .GetLevel(enterParams.Number)
                .As<PracticeLevelConfigs>();

            var taskSentenceRu = taskConfigs.SentenceRu;
            var taskSentenceEn = taskConfigs.SentenceEn;
            var cubeNumbersPool = taskConfigs.CubeNumbersPool.ToArray();

            // Slot bar.
            var slotBar = _slotBarFactory.Create(_slotBarPosition);
            yield return null; // To update slots layout. Forced update does not work.

            // Cubes.
            var cubesConfigsPool = cubesConfigs.GetCubes(cubeNumbersPool);
            slotBar.CreateCubes(cubesConfigsPool);

            // Task passing.
            _taskPassingService.SetCorrectSentence(taskSentenceEn);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            _ui.SetTaskSentence(taskSentenceRu);
            _ui.InitProgressBar();
            _ui.InitCubeRemoveArea();
            _ui.SetLevelTitle(taskConfigs);

            _taskPassingService.OnSentenceMatchingCalculated.AddListener(_ui.FillProgressBar);

            // Game completion.
            _taskPassingService.OnSentenceMatchingCalculated.AddListener((progress) =>
            {
                if (progress < 1 || _isLevelCompleted) return;
                _isLevelCompleted = true;

                _gameStateProvider.GameStateProxy.CompleteLevel(enterParams.Number);

                DOVirtual.DelayedCall(1, _gameplayPopUpProvider.OpenLevelCompletionPopUp);
            });

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}