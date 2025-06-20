using Configs;
using DG.Tweening;
using System.Collections.Generic;
using Theory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TheoryUI : SceneUI
    {
        [SerializeField] private TMP_Text _levelTitleView;
        [SerializeField] private ScrollRect _pageScroll;
        [SerializeField] private RectTransform _pageContainer;
        [SerializeField] private TheoryPage _pagePrefab;
        [SerializeField] private DotsProgressBar _progressBar;
        [SerializeField] private Transform _nextPageButton;

        private TheoryEnterParams _enterParams;
        private List<TheoryPage> _pages = new();
        private int _currentPageIndex;

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

            _nextPageButton.SetParent(_pageContainer, false);
            _pages[0].Show();
            //DOVirtual.DelayedCall(0.1f, () => _pageScroll.enabled = _pageContainer.anchoredPosition.y > Screen.height);
        }

        public void CreateProgressBar(int count)
        {
            _progressBar.CreateDots(count);
        }

        public void SwitchPage(int step)
        {
            var nextPageIndex = _currentPageIndex + step;

            if (nextPageIndex < 0)
            {
                nextPageIndex = 0;
            }
            else if (nextPageIndex >= _pages.Count)
            {
                CompleteTheory();
                return;
            }

            _pages[_currentPageIndex].Hide();
            _pages[nextPageIndex].Show();

            _pageContainer.anchoredPosition = new Vector2(_pageContainer.anchoredPosition.x, 0);
            //DOVirtual.DelayedCall(0.1f, () => _pageScroll.enabled = _pageContainer.anchoredPosition.y > Screen.height);

            _currentPageIndex = nextPageIndex;
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }

        public void OpenLevelMenu()
        {
            _sceneProvider.OpenLevelMenu(_enterParams);
        }

        private void CompleteTheory()
        {
            OpenLevelMenu();
        }
    }
}