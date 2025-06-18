using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private LevelButtonsLayout _levelButtonsLayout;

        public void CreateButtons()
        {
            var lastCompletedLevelNumber = GameState.LastCompletedLevelNumber;

            for (int i = 0; i < 20; i++)
            {
                var number = i + 1;

                var progress = LevelButtonProgress.Uncompleted; 
                if (number <= lastCompletedLevelNumber)
                    progress = LevelButtonProgress.Completed;
                else if (number == lastCompletedLevelNumber + 1)
                    progress = LevelButtonProgress.Current;

                var newButton = Instantiate(_levelButtonPrefab);
                newButton.Init((LevelButtonMode)Random.Range(0, 3), progress);

                _levelButtonsLayout.LayOut(newButton.GetComponent<RectTransform>(), i);
            }

            _levelButtonsLayout.ResizeContainer(20);
        }
    }
}