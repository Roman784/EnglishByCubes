using UnityEngine;
using Zenject;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private TaskUI _taskUI;
        [SerializeField] private ProgressBar _progressBar;

        [Space]

        [SerializeField] private Canvas _canvas;

        private void Start()
        {
            _canvas.worldCamera = Camera.main;
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

        public void FillProgressBar(float fill)
        {
            _progressBar.Fill(fill);
        }
    }
}