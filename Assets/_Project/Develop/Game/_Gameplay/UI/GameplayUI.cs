using Configs;
using Gameplay;
using GameRoot;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private TranslationSentences _sentences;
        [SerializeField] private CubeRemoveArea _cubeRemoveArea;
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private GameObject _infoContentPrefab;
        [SerializeField] private Canvas _canvas;

        private GameplayEnterParams _enterParams;
        private IGameFieldService _gameFieldService;

        [Inject]
        private void Construct( IGameFieldService gameFieldService)
        {
            _gameFieldService = gameFieldService;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.O))
                _popUpsProvider.OpenLevelCompletionPopUp(); // <-
        }

        public void Init(GameplayEnterParams enterParams)
        {
            _enterParams = enterParams;
            _canvas.worldCamera = Camera.main;
            _canvas.sortingLayerName = "UI";
        }

        public void CreateSentences(List<SentenceConfigs> sentencesData)
        {
            _sentences.CreateSentences(sentencesData);
        }

        public void ShowTranslation(int sentenceIdx)
        {
            _sentences.ShowTranslation(sentenceIdx);
        }

        public void InitCubeRemoveArea()
        {
            var configs = UIConfigs.CubeRemoveAreaConfigs;
            _cubeRemoveArea.Init(configs, _gameFieldService);
        }

        public void OpenLevelMenu()
        {
            _sceneProvider.OpenLevelMenu(_enterParams);
        }

        public void OpenTheory()
        {
            var theoryNumber = GameConfigs.LevelsConfigs.GetTheoryNumberForCurrentLevel(_enterParams.Number);
            _sceneProvider.OpenTheory(_enterParams, theoryNumber);
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title} {configs.LocalNumber}";
        }

        public void OpenCollection()
        {
            var nextGameplayEnterParams = new GameplayEnterParams(_enterParams.Number + 1);
            _sceneProvider.OpenCollection(nextGameplayEnterParams);
        }

        public void OpenNextLevel()
        {
            var nextLevelNumber = _enterParams.Number + 1;
            var levelsConfigs = GameConfigs.LevelsConfigs;
            var levelConfigs = levelsConfigs.GetLevel(nextLevelNumber);

            if (levelConfigs != null)
            {
                if (levelConfigs.Mode == LevelMode.Practice)
                    _sceneProvider.OpenPractice(_enterParams, nextLevelNumber);
                else if (levelConfigs.Mode == LevelMode.Template)
                    _sceneProvider.OpenTemplate(_enterParams, nextLevelNumber);
                else if (levelConfigs.Mode == LevelMode.MistakeCorrection)
                    _sceneProvider.OpenMistakeCorrection(_enterParams, nextLevelNumber);
            }
            else
            {
                _sceneProvider.OpenLevelMenu(_enterParams);
            }
        }

        public void OpenLevelInfo()
        {
            _popUpsProvider.OpenLevelInfo(_infoContentPrefab);
        }
    }
}