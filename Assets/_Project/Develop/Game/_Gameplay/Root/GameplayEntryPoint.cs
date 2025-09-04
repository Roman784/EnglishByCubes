using Configs;
using DG.Tweening;
using GameRoot;
using System.Collections;
using System.Linq;
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

        private bool _isLevelCompleted;

        [Inject]
        private void Construct(GameplayUI ui,
                               SlotBarFactory slotBarFactory,
                               ILevelPassingService levelPassingService)
        {
            _ui = ui;
            _slotBarFactory = slotBarFactory;
            _levelPassingService = (GameplayLevelPassingService)levelPassingService;
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

            var cubeNumbersPool = taskConfigs.CubeNumbersPool.ToArray();

            // Slot bar.
            var slotBar = _slotBarFactory.Create(_slotBarPosition);
            yield return null; // To update slots layout. Forced update does not work.

            // Cubes.
            var cubesConfigsPool = cubesConfigs.GetCubes(cubeNumbersPool);
            slotBar.CreateCubes(cubesConfigsPool);

            // Task passing.
            var correctSentences = taskConfigs.Sentences.Select(s => s.TargetSentence).ToList();
            _levelPassingService.SetCorrectSentences(correctSentences);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            _ui.InitCubeRemoveArea();
            _ui.SetLevelTitle(taskConfigs);

            _ui.CreateSentences(taskConfigs.Sentences);
            _levelPassingService.OnSentenceMatchingCalculated.AddListener((sentenceIdx, completedSentences) =>
            {
                _ui.ShowTranslation(sentenceIdx);
            });

            // Game completion.
            _levelPassingService.OnSentenceMatchingCalculated.AddListener((sentenceIdx, completedSentences) =>
            {
                if (completedSentences < correctSentences.Count)
                {
                    PlaySentenceCompletedSound();
                    return;
                }

                if (_isLevelCompleted) return;
                _isLevelCompleted = true;

                _gameStateProvider.GameStateProxy.CompleteLevel(enterParams.Number);

                PlayLevelCompletionSound();
                DOVirtual.DelayedCall(1, _rootPopUpsProvider.OpenLevelCompletionPopUp);
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