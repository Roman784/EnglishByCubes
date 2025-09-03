using Configs;
using Gameplay;
using MistakeCorrection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class MistakeCorrectionUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _infoContentPrefab;

        private MistakeCorrectionEnterParams _enterParams;

        public void Init(MistakeCorrectionEnterParams enterParams)
        {
            _enterParams = enterParams;
            _canvas.worldCamera = Camera.main;
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

        public void OpenCollection()
        {
            var nextGameplayEnterParams = new GameplayEnterParams(_enterParams.Number + 1);
            _sceneProvider.OpenCollection(nextGameplayEnterParams);
        }

        public void OpenNextLevel()
        {
            var nextLevelNumber = _enterParams.Number + 1;
            var levelsConfigs = GameConfigs.LevelsConfigs;
            var levelConfigs = levelsConfigs.GetLevel(nextLevelNumber);

            if (levelConfigs != null && levelConfigs.Mode == LevelMode.Practice)
                _sceneProvider.OpenPractice(_enterParams, nextLevelNumber);
            else
                _sceneProvider.OpenLevelMenu(_enterParams);
        }

        public void OpenLevelInfo()
        {
            _popUpsProvider.OpenLevelInfo(_infoContentPrefab);
        }
    }
}