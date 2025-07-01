using Configs;
using GameRoot;
using System.Collections.Generic;
using Template;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class TemplateUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private List<TMP_Text> _sentenceViews;

        [Space]

        [SerializeField] private Canvas _canvas;

        private TemplateEnterParams _enterParams;
        private TemplateLevelPassingService _levelPassingService;

        private int _currentSentenceIndex;

        [Inject]
        private void Construct(ILevelPassingService levelPassingService)
        {
            _levelPassingService = (TemplateLevelPassingService)levelPassingService;

            _levelPassingService.OnNewSentenceFounded.AddListener(ShowNewSentence);
        }

        public void Init(TemplateEnterParams enterParams)
        {
            _enterParams = enterParams;
            _canvas.worldCamera = Camera.main;
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

        private void ShowNewSentence(TemplateSentence sentence, int _)
        {
            if (_currentSentenceIndex >= _sentenceViews.Count) return;

            var view = _sentenceViews[_currentSentenceIndex];
            var text = $"{sentence.SentenceEn}\n{sentence.SentenceRu}";

            view.transform.localScale = Vector3.one;
            view.alignment = TextAlignmentOptions.Left;
            view.text = text;

            _currentSentenceIndex += 1;
        }
    }
}