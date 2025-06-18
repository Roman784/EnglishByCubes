using UnityEngine;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private GameObject _completedButton;
        [SerializeField] private GameObject _uncompletedButton;
        [SerializeField] private GameObject _currentButton;

        public void Init(LevelButtonProgress mode)
        {
            _completedButton.SetActive(mode == LevelButtonProgress.Completed);
            _uncompletedButton.SetActive(mode == LevelButtonProgress.Uncompleted);
            _currentButton.SetActive(mode == LevelButtonProgress.Current);
        }
    }
}