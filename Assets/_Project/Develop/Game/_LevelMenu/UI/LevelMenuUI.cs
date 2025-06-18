using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private LevelButtonsLayout _levelButtonsLayout;

        public void CreateButtons()
        {
            for (int i = 0; i < 10; i++)
            {
                var newButton = Instantiate(_levelButtonPrefab);

                if (i % 3 == 0)
                    newButton.Init(LevelButtonProgress.Current);
                if (i % 3 == 1)
                    newButton.Init(LevelButtonProgress.Completed);
                if (i % 3 == 2)
                    newButton.Init(LevelButtonProgress.Uncompleted);

                _levelButtonsLayout.LayOut(newButton.GetComponent<RectTransform>(), i);
            }

            _levelButtonsLayout.ResizeContainer(10);
        }
    }
}