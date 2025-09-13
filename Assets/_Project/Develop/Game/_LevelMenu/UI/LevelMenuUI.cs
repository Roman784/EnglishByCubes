using LevelMenu;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private RectTransform _levelButtonsContainer;

        [Space]

        [SerializeField] private TMP_Text _sectionTitleView;
        [SerializeField] private TMP_Text _levelTitleView;

        [Space]

        [SerializeField] private Canvas _canvas;

        private LevelMenuEnterParams _enterParams;
        private LevelButtonsLayout _levelButtonsLayout;

        public void Init(LevelMenuEnterParams enterParams)
        {
            _canvas.worldCamera = Camera.main;
            _canvas.planeDistance = 1;

            _enterParams = enterParams;
            _levelButtonsLayout = new(UIConfigs.LevelButtonsConfigs, _levelButtonsContainer);

            InitScrollButtonView();
        }

        public void CreateButtons()
        {
            var buttonsConfigs = UIConfigs.LevelButtonsConfigs;
            var levels = GameConfigs.LevelsConfigs.Levels;
            var lastCompletedLevelNumber = GameState.LastCompletedLevelNumber;

            foreach (var level in levels)
            {
                var progress = GetLevelProgress(level.GlobalNumber, lastCompletedLevelNumber);

                var newButton = Instantiate(_levelButtonPrefab);
                newButton.Init(level, buttonsConfigs, progress, _audioProvider, AudioConfigs.ButtonClickSound, OpenLevel);

                _levelButtonsLayout.LayOut(newButton.GetComponent<RectTransform>(), level.GlobalNumber - 1);
            }

            _levelButtonsLayout.ResizeContainer(levels.Count);
        }

        public void ScrollToCurrentButton(bool instantly = false)
        {
            var lastCompletedLevelNumber = GameState.LastCompletedLevelNumber;
            _levelButtonsLayout.ScrollTo(lastCompletedLevelNumber, instantly);
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }

        private void InitScrollButtonView()
        {
            var currentLevelNumber = GameState.LastCompletedLevelNumber + 1;
            var levelConfigs = GameConfigs.LevelsConfigs.GetLevel(currentLevelNumber);

            if (levelConfigs == null)
            {
                _sectionTitleView.text = "";
                _levelTitleView.text = "Всё пройдено!";
                return;
            }

            _sectionTitleView.text = levelConfigs.SectionTitle;
            _levelTitleView.text = levelConfigs.Title;
        }

        public void OpenCollection()
        {
            _sceneProvider.OpenCollection(_enterParams);
        }

        private void OpenLevel(int number)
        {
            var levels = GameConfigs.LevelsConfigs;
            var level = levels.GetLevel(number);

            switch (level.Mode)
            {
                case LevelMode.Theory:
                    _sceneProvider.OpenTheory(_enterParams, number);
                    break;
                case LevelMode.Template:
                    _sceneProvider.OpenTemplate(_enterParams, number);
                    break;
                case LevelMode.Practice:
                    _sceneProvider.OpenPractice(_enterParams, number);
                    break;
                case LevelMode.MistakeCorrection:
                    _sceneProvider.OpenMistakeCorrection(_enterParams, number);
                    break;
            }
        }

        private LevelButtonProgress GetLevelProgress(int currentNumber, int lastCompletedNumber)
        {
            if (currentNumber <= lastCompletedNumber)
                return LevelButtonProgress.Completed;
            else if (currentNumber == lastCompletedNumber + 1)
                return LevelButtonProgress.Current;

            return LevelButtonProgress.Uncompleted;
        }
    }
}