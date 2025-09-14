using Configs;
using DG.Tweening;
using Gameplay;
using GameRoot;
using Template;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class TemplateUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private TemplateSentences _sentences;

        [Space]

        [SerializeField] private GameObject _infoContentPrefab;

        [Space]

        [SerializeField] private Canvas _canvas;

        private TemplateEnterParams _enterParams;
        private TemplateLevelPassingService _levelPassingService;

        [Inject]
        private void Construct(ILevelPassingService levelPassingService)
        {
            _levelPassingService = (TemplateLevelPassingService)levelPassingService;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.O))
                _popUpsProvider.OpenLevelCompletionPopUp(); // <-
        }

        public void Init(TemplateEnterParams enterParams, int sentencesCount)
        {
            _enterParams = enterParams;
            _canvas.worldCamera = Camera.main;
            _canvas.sortingLayerName = "UI";

            _sentences.CreateSentences(sentencesCount);

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