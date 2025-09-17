using Configs;
using DG.Tweening;
using GameRoot;
using R3;
using System.Collections;
using System.Linq;
using UI;
using UnityEngine;
using Utils;
using Zenject;

namespace Template
{
    public class TemplateEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Vector3 _templateSlotsPosition;

        private TemplateUI _ui;
        private TemplateSlotsFactory _templateSlotsFactory;
        private TemplateFieldService _gameFieldService;
        private TemplateLevelPassingService _levelPassingService;

        private bool _isLevelCompleted;

        [Inject]
        private void Construct(TemplateUI ui,
                               TemplateSlotsFactory templateSlotsFactory,
                               IGameFieldService gameFieldService,
                               ILevelPassingService levelPassingService)
        {
            _ui = ui;
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

            // Template slots.
            var templateSlots = _templateSlotsFactory.Create(_templateSlotsPosition);
            templateSlots.CreateSlots(levelConfigs.Slots);
            yield return null; // To update slots layout. Forced update does not work.

            // Cubes.
            var cubes = cubesConfigs.GetCubes(levelConfigs.Slots.Select(s => s.CubeNumber).ToArray());
            Coroutines.Start(templateSlots.CreateCubes(cubes));

            // Services.
            _levelPassingService.SetTargetSentences(levelConfigs.Sentences, levelConfigs.SentencesCount);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams, levelConfigs.SentencesCount);
            _ui.SetLevelTitle(levelConfigs);

            _levelPassingService.OnWordsInBanList.AddListener(words =>
            {
                _ui.ShowBanWords(words);
            });

            _levelPassingService.OnNewSentenceFounded.AddListener((_, sentencesLeft) =>
            {
                if (sentencesLeft > 0)
                {
                    PlaySentenceCompletedSound();
                    return;
                }

                if (_isLevelCompleted) return;
                _isLevelCompleted = true;

                _gameStateProvider.GameStateProxy.CompleteLevel(enterParams.Number);

                PlayLevelCompletionSound();
                _ui.ShowLevelCompleteButton();
            });

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }

        private void PlayLevelCompletionSound()
        {
            var clip = _configsProvider.GameConfigs.AudioConfigs.UIConfigs.LevelCompletionSound;
            _audioProvider.PlaySound(clip);
        }

        private void PlaySentenceCompletedSound()
        {
            var clip = _configsProvider.GameConfigs.AudioConfigs.UIConfigs.SentenceCompletedSound;
            _audioProvider.PlaySound(clip);
        }
    }
}