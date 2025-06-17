using UnityEngine;

namespace UI
{
    public class LevelMenuUI : SceneUI
    {
        [SerializeField] private GameObject _levelButtonPrefab;
        [SerializeField] private LevelButtonsLayout _levelButtonsLayout;

        public void CreateButtons()
        {
            for (int i = 0; i < 10; i++)
            {
                var newButton = Instantiate(_levelButtonPrefab);
                _levelButtonsLayout.LayOut(newButton.GetComponent<RectTransform>(), i);   
            }

            _levelButtonsLayout.ResizeContainer(10);
        }
    }
}