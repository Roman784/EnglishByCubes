using UnityEngine;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private TaskUI _taskUI;
        [SerializeField] private ProgressBar _progressBar;

        private int _textCount = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _textCount -= 1;
                _progressBar.Fill(_textCount, 10);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _textCount += 1;
                _progressBar.Fill(_textCount, 10);
            }
        }

        public void SetTaskSentence(string sentence)
        {
            _taskUI.SetSentence(sentence);
        }
    }
}