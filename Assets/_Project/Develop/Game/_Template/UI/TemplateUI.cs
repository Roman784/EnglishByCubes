using Configs;
using Template;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TemplateUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;

        [Space]

        [SerializeField] private Canvas _canvas;

        private TemplateEnterParams _enterParams;

        public void Init(TemplateEnterParams enterParams)
        {
            _enterParams = enterParams;
            _canvas.worldCamera = Camera.main;
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title} {configs.LocalNumber}";
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
    }
}