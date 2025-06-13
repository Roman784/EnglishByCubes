using Gameplay;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private TaskUI _taskUI;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private CubeRemoveArea _cubeRemoveArea;

        [Space]

        [SerializeField] private Canvas _canvas;

        private GameFieldService _gameFieldService;

        [Inject]
        private void Construct(GameFieldService gameFieldService)
        {
            _gameFieldService = gameFieldService;
        }

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
            var configs = UIConfigs.GameplayProgressBarConfigs;
            _progressBar.Init(configs);
        }

        public void InitCubeRemoveArea()
        {
            var configs = GameConfigs.CubesConfigs.CubeRemoveAreaConfigs;
            _cubeRemoveArea.Init(configs, _gameFieldService);
        }

        public void FillProgressBar(float fill)
        {
            _progressBar.Fill(fill);
        }
    }
}