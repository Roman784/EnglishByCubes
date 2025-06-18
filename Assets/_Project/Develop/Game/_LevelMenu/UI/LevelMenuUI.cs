using Gameplay;
using GameRoot;
using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private LevelButtonsLayout _levelButtonsLayout;

        public void Init()
        {
            _levelButtonsLayout.Init(UIConfigs.LevelButtonsConfigs);
        }

        public void CreateButtons()
        {
            var buttonsConfigs = UIConfigs.LevelButtonsConfigs;
            var levels = GameConfigs.LevelsConfigs.Levels;
            var lastCompletedLevelNumber = GameState.LastCompletedLevelNumber;

            foreach (var level in levels)
            {
                var progress = GetLevelProgress(level.Number, lastCompletedLevelNumber);

                var newButton = Instantiate(_levelButtonPrefab);
                newButton.Init(level, buttonsConfigs, progress, OpenLevel);

                _levelButtonsLayout.LayOut(newButton.GetComponent<RectTransform>(), level.Number - 1);
            }

            _levelButtonsLayout.ResizeContainer(levels.Count);
        }

        public void ScrollToCurrentButton(bool instantly = false)
        {
            var lastCompletedLevelNumber = GameState.LastCompletedLevelNumber;
            _levelButtonsLayout.ScrollTo(lastCompletedLevelNumber, instantly);
        }

        private void OpenLevel(int number)
        {
            var levels = GameConfigs.LevelsConfigs;
            var level = levels.GetLevel(number);

            if (level.Mode == LevelMode.Theory)
                OpenPracticeLevel(3);
            else if (level.Mode == LevelMode.Template)
                OpenPracticeLevel(3);
            else if (level.Mode == LevelMode.Practice)
                OpenPracticeLevel(number);
        }

        private void OpenPracticeLevel(int number)
        {
            var enterParams = new GameplayEnterParams(Scenes.LEVEL_MENU, number);
            _sceneLoader.LoadAndRunGameplay(enterParams);
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