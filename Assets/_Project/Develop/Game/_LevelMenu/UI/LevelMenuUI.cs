using Gameplay;
using GameRoot;
using LevelMenu;
using Theory;
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
            if (_enterParams.ExitSceneName == Scenes.GAMEPLAY)
            {
                var gameplayEnterParams = new GameplayEnterParams(Scenes.LEVEL_MENU, 
                                                                  _enterParams.CurrentLevelNumber);
                _sceneLoader.LoadAndRunGameplay(gameplayEnterParams);
            }
        }

        private void OpenLevel(int number)
        {
            var levels = GameConfigs.LevelsConfigs;
            var level = levels.GetLevel(number);

            if (level.Mode == LevelMode.Theory)
                OpenTheoryLevel(number);
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

        private void OpenTheoryLevel(int number)
        {
            var enterParams = new TheoryEnterParams(Scenes.LEVEL_MENU, number);
            _sceneLoader.LoadAndRunTheory(enterParams);
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