using Configs;
using Gameplay;
using GameRoot;
using MistakeCorrection;
using Template;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MistakeCorrectionUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _infoContentPrefab;
        [SerializeField] private TemplateSentences _sentences;

        private MistakeCorrectionEnterParams _enterParams;
        private MistakeCorrectionLevelPassingService _levelPassingService;

        [Inject]
        private void Construct(ILevelPassingService levelPassingService)
        {
            _levelPassingService = (MistakeCorrectionLevelPassingService)levelPassingService;
        }

        public void Init(MistakeCorrectionEnterParams enterParams, int sentencesCount)
        {
            _enterParams = enterParams;
            _canvas.worldCamera = Camera.main;

            _sentences.CreateSentences(sentencesCount);

            _levelPassingService.OnNewSentenceFounded.AddListener((sentence, sentencesLeft) =>
            {
                _sentences.ShowNewSentence(sentence);
            });
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

            if (levelConfigs != null && levelConfigs.Mode == LevelMode.Practice)
                _sceneProvider.OpenPractice(_enterParams, nextLevelNumber);
            else
                _sceneProvider.OpenLevelMenu(_enterParams);
        }

        public void OpenLevelInfo()
        {
            _popUpsProvider.OpenLevelInfo(_infoContentPrefab);
        }
    }
}