using Configs;
using Theory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TheoryUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;

        private TheoryEnterParams _enterParams;

        public void Init(TheoryEnterParams enterParams)
        {
            _enterParams = enterParams;
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title} {configs.LocalNumber}";
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }

        public void OpenLevelMenu()
        {
            _sceneProvider.OpenLevelMenu(_enterParams);
        }
    }
}