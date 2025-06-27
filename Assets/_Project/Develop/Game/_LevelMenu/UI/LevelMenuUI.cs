using LevelMenu;
using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private RectTransform _levelButtonsContainer;

        private LevelMenuEnterParams _enterParams;
        private LevelButtonsLayout _levelButtonsLayout;

        public void Init(LevelMenuEnterParams enterParams)
        {
            _enterParams = enterParams;

            _levelButtonsLayout = new(UIConfigs.LevelButtonsConfigs, _levelButtonsContainer);
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
                newButton.Init(level, buttonsConfigs, progress, OpenLevel);

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
                    break;
                case LevelMode.Practice:
                    _sceneProvider.OpenPractice(_enterParams, number);
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