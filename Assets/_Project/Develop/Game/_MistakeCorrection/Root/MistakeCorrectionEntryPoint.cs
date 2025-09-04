using Configs;
using DG.Tweening;
using Gameplay;
using GameRoot;
using R3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Utils;
using Zenject;

namespace MistakeCorrection
{
    public class MistakeCorrectionEntryPoint : SceneEntryPoint
    {
        private MistakeCorrectionUI _ui;
        private CubeFactory _cubeFactory;
        private MistakeCorrectionFieldService _gameFieldService;
        private MistakeCorrectionLevelPassingService _levelPassingService;

        private int _currentSentenceIdx = 0;
        private List<Cube> _cubesOnField = new();
        private bool _isLevelCompleted;

        [Inject]
        private void Construct(MistakeCorrectionUI ui, CubeFactory cubeFactory, 
                               IGameFieldService gameFieldService, ILevelPassingService levelPassingService)
        {
            _ui = ui;
            _cubeFactory = cubeFactory;
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
            CreateCubes(levelConfigs, cubesConfigs);

            // Services.
            _levelPassingService.SetTargetSentences(levelConfigs.Sentences);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams, levelConfigs.Sentences.Count);
            _ui.SetLevelTitle(levelConfigs);

            _levelPassingService.OnNewSentenceFounded.AddListener((_, sentencesLeft) =>
            {
                if (sentencesLeft > 0)
                {
                    _currentSentenceIdx += 1;
                    UpdateCubes(levelConfigs, cubesConfigs);
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

        private void UpdateCubes(MistakeCorrectionConfigs levelConfigs, CubesConfigs cubesConfigs)
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                DestroyCubes();

                DOVirtual.DelayedCall(1f, () =>
                {
                    CreateCubes(levelConfigs, cubesConfigs);
                });
            });
        }
        private void CreateCubes(MistakeCorrectionConfigs levelConfigs, CubesConfigs cubesConfigs)
        {
            var sentence = levelConfigs.Sentences[_currentSentenceIdx];
            var cubeNumbers = sentence.Slots.Select(s => s.CubeNumber).ToArray();
            var cubeWordIndexes = sentence.Slots.Select(s => s.SideIndex).ToArray();
            var cubeConfigs = cubesConfigs.GetCubes(cubeNumbers);

            Coroutines.Start(CreateCubesOnField(cubeConfigs, cubeWordIndexes));
        }

        private IEnumerator CreateCubesOnField(List<CubeConfigs> cubeConfigs, int[] wordIndexes)
        {
            for (int i = 0; i < cubeConfigs.Count; i++)
            {
                var config = cubeConfigs[i];
                var cube = _cubeFactory.Create(config);
                var cubeOnField = cube.CreateOnField();
                cube.DisableInSlots(true);
                cube.Destroy();

                cubeOnField.Rotate(wordIndexes[i]);
                _cubesOnField.Add(cubeOnField);

                yield return new WaitForSeconds(0.05f);
            }
        } 

        private void DestroyCubes()
        {
            if (_cubesOnField.Count == 0) return;

            foreach (var cube in _cubesOnField)
            {
                cube.RemoveFromField();
            }
            _cubesOnField = new List<Cube>();
        }

        private void PlayLevelCompletionSound()
        {
            var clip = _configsProvider.GameConfigs.AudioConfigs.UIConfigs.LevelCompletionSound;
            _audioProvider.PlaySound(clip);
        }
    }
}