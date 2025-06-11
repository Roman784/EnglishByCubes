using UnityEngine;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private TaskUI _taskUI;
        [SerializeField] private ProgressBar _progressBar;

        [Space]

        [SerializeField] private Canvas _canvas;

        private int _textCount = 0;

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
        }

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

        public void InitProgressBar()
        {
            var configs = ThemeConfigs.ProgressBarConfigs;
            _progressBar.Init(configs);
        }
    }
}