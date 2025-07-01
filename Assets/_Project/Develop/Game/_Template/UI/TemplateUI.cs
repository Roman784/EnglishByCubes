using Configs;
using Gameplay;
using GameRoot;
using System.Collections.Generic;
using Template;
using TMPro;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

namespace UI
{
    public class TemplateUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private TemplateSentences _sentences;
        [SerializeField] private CubeRemoveArea _cubeRemoveArea;

        [Space]

        [SerializeField] private GameObject _infoContentPrefab;

        [Space]

        [SerializeField] private Canvas _canvas;

        private TemplateEnterParams _enterParams;
        private TemplateLevelPassingService _levelPassingService;
        private IGameFieldService _gameFieldService;

        [Inject]
        private void Construct(ILevelPassingService levelPassingService, IGameFieldService gameFieldService)
        {
            _levelPassingService = (TemplateLevelPassingService)levelPassingService;
            _gameFieldService = gameFieldService;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.O))
                _popUpsProvider.OpenLevelCompletionPopUp(); // <-
        }

        public void Init(TemplateEnterParams enterParams)
        {
            _enterParams = enterParams;
            _canvas.worldCamera = Camera.main;

            var configs = UIConfigs.CubeRemoveAreaConfigs;
            _cubeRemoveArea.Init(configs, _gameFieldService);

            _levelPassingService.OnNewSentenceFounded.AddListener((sentence, sentencesLeft) =>
            {
                _sentences.ShowNewSentence(sentence);
            });
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title} {configs.LocalNumber}";
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