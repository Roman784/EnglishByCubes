using Configs;
using Gameplay;
using GameRoot;
using LevelMenu;
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
            var configs = UIConfigs.CubeRemoveAreaConfigs;
            _cubeRemoveArea.Init(configs, _gameFieldService);
        }

        public void FillProgressBar(float fill)
        {
            _progressBar.Fill(fill);
        }

        public void OpenLevelMenu()
        {
            var enterParams = new LevelMenuEnterParams(Scenes.GAMEPLAY);
            _sceneLoader.LoadAndRunLevelMenu(enterParams);
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title} {configs.LocalNumber}";
        }
    }
}