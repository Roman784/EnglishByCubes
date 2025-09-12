using Configs;
using DG.Tweening;
using System.Collections.Generic;
using Theory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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

        private TheoryLevelConfigs _levelConfigs;

        private TheoryPopUpProvider _theoryPopUpProvider;

        [Inject]
        private void Construct(TheoryPopUpProvider theoryPopUpProvider)
        {
            _theoryPopUpProvider = theoryPopUpProvider;
        }

        public void Init(TheoryEnterParams enterParams)
        {
            _enterParams = enterParams;
        }

        public void SetLevelTitle(LevelConfigs configs)
        {
            _levelTitleView.text = $"{configs.Title}";
        }

        public void CreatePages(TheoryLevelConfigs configs)
        {
            _levelConfigs = configs;

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
            _progressBar.HighlightDot(0);
        }

        public void CreateProgressBar(int count)
        {
            _progressBar.CreateDots(count);
        }

        public void SwitchPageByIndex(int index)
        {
            OpenPage(index);
        }

        public void SwitchPage(int step)
        {
            var nextPageIndex = _currentPageIndex + step;
            OpenPage(nextPageIndex);
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }

        public void OpenLevelMenu()
        {
            _sceneProvider.OpenLevelMenu(_enterParams);
        }

        public void OpenTableOfContentsPopUp()
        {
            _theoryPopUpProvider.OpenTableOfContents(_levelConfigs);
        }

        private void OpenPage(int pageIndex)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            else if (pageIndex >= _pages.Count)
            {
                CompleteTheory();
                return;
            }

            _pages[_currentPageIndex].Hide();
            _pages[pageIndex].Show();

            _pageContainer.anchoredPosition = new Vector2(_pageContainer.anchoredPosition.x, 0);

            _progressBar.HighlightDot(pageIndex);

            _currentPageIndex = pageIndex;
        }

        private void CompleteTheory()
        {
            PlayLevelCompletionSound();

            _gameStateProvider.GameStateProxy.CompleteLevel(_enterParams.Number);
            OpenLevelMenu();
        }

        private void PlayLevelCompletionSound()
        {
            var clip = _configsProvider.GameConfigs.AudioConfigs.UIConfigs.LevelCompletionSound;
            _audioProvider.PlaySound(clip);
        }
    }
}