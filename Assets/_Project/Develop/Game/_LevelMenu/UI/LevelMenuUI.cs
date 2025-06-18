using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private LevelButtonsLayout _levelButtonsLayout;

        public void CreateButtons()
        {
            var levels = GameConfigs.LevelsConfigs.Levels;
            var lastCompletedLevelNumber = GameState.LastCompletedLevelNumber;

            foreach (var level in levels)
            {
                var progress = LevelButtonProgress.Uncompleted; 
                if (level.Number <= lastCompletedLevelNumber)
                    progress = LevelButtonProgress.Completed;
                else if (level.Number == lastCompletedLevelNumber + 1)
                    progress = LevelButtonProgress.Current;

                var newButton = Instantiate(_levelButtonPrefab);
                newButton.Init(level.Mode, progress);

                _levelButtonsLayout.LayOut(newButton.GetComponent<RectTransform>(), level.Number - 1);
            }

            _levelButtonsLayout.ResizeContainer(levels.Count);
        }

        public void ScrollToCurrentButton(bool instantly = false)
        {
            var lastCompletedLevelNumber = GameState.LastCompletedLevelNumber;
            _levelButtonsLayout.ScrollTo(lastCompletedLevelNumber, instantly);
        }
    }
}