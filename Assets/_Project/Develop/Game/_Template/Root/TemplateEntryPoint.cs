using Configs;
using Gameplay;
using GameRoot;
using System;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Template
{
    public class TemplateEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Vector3 _slotBarPosition;
        [SerializeField] private Vector3 _templateSlotsPosition;

        private TemplateUI _ui;
        private SlotBarFactory _slotBarFactory;
        private TemplateSlotsFactory _templateSlotsFactory;
        private TemplateFieldService _gameFieldService;
        private TemplateLevelPassingService _levelPassingService;

        private bool _isLevelCompleted;

        [Inject]
        private void Construct(TemplateUI ui,
                               SlotBarFactory slotBarFactory,
                               TemplateSlotsFactory templateSlotsFactory,
                               IGameFieldService gameFieldService,
                               ILevelPassingService levelPassingService)
        {
            _ui = ui;
            _slotBarFactory = slotBarFactory;
            _templateSlotsFactory = templateSlotsFactory;
            _gameFieldService = (TemplateFieldService)gameFieldService;
            _levelPassingService = (TemplateLevelPassingService)levelPassingService;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<TemplateEnterParams>());
        }

        private IEnumerator Run(TemplateEnterParams enterParams)
        {
            var isLoaded = false;

            var gameConfigs = _configsProvider.GameConfigs;
            var cubesConfigs = gameConfigs.CubesConfigs;
            var levelsConfigs = gameConfigs.LevelsConfigs;

            var levelConfigs = levelsConfigs
                .GetLevel(enterParams.Number)
                .As<TemplateLevelConfigs>();

            var cubeNumbersPool = levelConfigs.CubeNumbersPool.ToArray();

            // Slot bar.
            var slotBar = _slotBarFactory.Create(_slotBarPosition);
            yield return null; // To update slots layout. Forced update does not work.

            // Template slots.
            var templateSlots = _templateSlotsFactory.Create(_templateSlotsPosition);
            templateSlots.CreateSlots(levelConfigs.Slots);
            yield return null; // To update slots layout. Forced update does not work.

            // Cubes.
            var cubesConfigsPool = cubesConfigs.GetCubes(cubeNumbersPool);
            slotBar.CreateCubes(cubesConfigsPool);

            // Services.
            _gameFieldService.SetMaxCubeCount(levelConfigs.Slots.Count);
            _levelPassingService.SetTargetSentences(levelConfigs.Sentences);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            _ui.SetLevelTitle(levelConfigs);

            _levelPassingService.OnNewSentenceFounded.AddListener((_, sentencesLeft) =>
            {
                if (sentencesLeft > 0 || _isLevelCompleted) return;
                _isLevelCompleted = true;

                _gameStateProvider.GameStateProxy.CompleteLevel(enterParams.Number);
            });

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}