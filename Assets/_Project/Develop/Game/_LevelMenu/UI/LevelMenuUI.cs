using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private LevelButtonsLayout _levelButtonsLayout;

        public void CreateButtons()
        {
            for (int i = 0; i < 20; i++)
            {
                var newButton = Instantiate(_levelButtonPrefab);
                newButton.Init((LevelButtonMode)Random.Range(0, 3), (LevelButtonProgress)Random.Range(0, 3));

                _levelButtonsLayout.LayOut(newButton.GetComponent<RectTransform>(), i);
            }

            _levelButtonsLayout.ResizeContainer(20);
        }
    }
}