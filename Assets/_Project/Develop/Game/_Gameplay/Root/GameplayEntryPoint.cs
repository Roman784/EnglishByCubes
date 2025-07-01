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
        private GameplayLevelPassingService _levelPassingService;
        private GameplayPopUpProvider _gameplayPopUpProvider;

        private bool _isLevelCompleted;

        [Inject]
        private void Construct(GameplayUI ui,
                               SlotBarFactory slotBarFactory,
                               ILevelPassingService levelPassingService,
                               GameplayPopUpProvider gameplayPopUpProvider)
        {
            _ui = ui;
            _slotBarFactory = slotBarFactory;
            _levelPassingService = (GameplayLevelPassingService)levelPassingService;
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
            _levelPassingService.SetCorrectSentence(taskSentenceEn);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            _ui.SetTaskSentence(taskSentenceRu);
            _ui.InitProgressBar();
            _ui.InitCubeRemoveArea();
            _ui.SetLevelTitle(taskConfigs);

            _levelPassingService.OnSentenceMatchingCalculated.AddListener(_ui.FillProgressBar);

            // Game completion.
            _levelPassingService.OnSentenceMatchingCalculated.AddListener((progress) =>
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