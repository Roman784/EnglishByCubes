using Configs;
using System.Collections.Generic;
using Theory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TheoryUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private Transform _pageContainer;
        [SerializeField] private TheoryPage _pagePrefab;
        [SerializeField] private DotsProgressBar _progressBar;

        private TheoryEnterParams _enterParams;
        private List<TheoryPage> _pages = new(); 

        public void Init(TheoryEnterParams enterParams)
        {
            _enterParams = enterParams;
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title} {configs.LocalNumber}";
        }

        public void CreatePages(TheoryLevelConfigs configs)
        {
            foreach (var pageConfigs in configs.PagesConfigs)
            {
                var newPage = Instantiate(_pagePrefab);
                newPage.transform.SetParent(_pageContainer, false);

                newPage.CreateContent(pageConfigs);
                newPage.Hide(true);

                _pages.Add(newPage);
            }
        }

        public void CreateProgressBar(int count)
        {
            _progressBar.CreateDots(count);
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