using Configs;
using Gameplay;
using TMPro;
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

        [SerializeField] private TMP_Text _levelTitleView;

        [Space]

        [SerializeField] private Canvas _canvas;

        private GameplayEnterParams _enterParams;
        private GameFieldService _gameFieldService;

        [Inject]
        private void Construct(GameFieldService gameFieldService)
        {
            _gameFieldService = gameFieldService;
        }

        public void Init(GameplayEnterParams enterParams)
        {
            _enterParams = enterParams;
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
            var configs = UIConfigs.CubeRemoveAreaConfigs;
            _cubeRemoveArea.Init(configs, _gameFieldService);
        }

        public void FillProgressBar(float fill)
        {
            _progressBar.Fill(fill);
        }

        public void OpenLevelMenu()
        {
            _sceneProvider.OpenLevelMenu(_enterParams);
        }

        public void OpenTheory()
        {
            var theoryNumber = GameConfigs.LevelsConfigs.GetTheoryNumberForCurrentLevel(_enterParams.Number);
            _sceneProvider.OpenTheory(_enterParams, theoryNumber);
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title} {configs.LocalNumber}";
        }
    }
}