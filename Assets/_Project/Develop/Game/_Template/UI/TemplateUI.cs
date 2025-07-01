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
    }
}