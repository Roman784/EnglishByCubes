using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class AppInfoPopUp : PopUp
    {
        [Space]

        [SerializeField] private List<GameObject> _pages;
        [SerializeField] private RectTransform _pageContainer;

        [Space]

        [SerializeField] private DotsProgressBar _progressBar;

        private int _currentPageIndex;

        private new void Awake()
        {
            _progressBar.CreateDots(_pages.Count);

            base.Awake();
        }

        public override void Open()
        {
            foreach (var page in _pages)
            {
                page.SetActive(false);
            }
            _pages[0].SetActive(true);

            _progressBar.HighlightDot(0);

            base.Open();
        }

        public void SwitchPage(int step)
        {
            var nextPageIndex = _currentPageIndex + step;
            nextPageIndex = Mathf.Clamp(nextPageIndex, 0, _pages.Count - 1);

            _pages[_currentPageIndex].SetActive(false);
            _pages[nextPageIndex].SetActive(true);

            _pageContainer.anchoredPosition = new Vector2(_pageContainer.anchoredPosition.x, 0);

            _progressBar.HighlightDot(nextPageIndex);

            _currentPageIndex = nextPageIndex;
        }

        public class Factory : PopUpFactory<AppInfoPopUp>
        {
        }
    }
}