using Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TheoryTableOfContentsPopUp : PopUp
    {
        [Space]

        [SerializeField] private Image _fade;
        [SerializeField] private float _fadeValue;
        [SerializeField] private Ease _fadeTransparencyEase;

        [Space]

        [SerializeField] private RectTransform _container;
        [SerializeField] private TheoryPageLink _pageLinkPrefab;

        private new void Awake()
        {
            SetTransparency(_fade, 0f, 0, 0);
            SetScale(_view.transform, 0f, 0, 0);
        }

        public void Init(TheoryLevelConfigs configs)
        {
            var pages = configs.PagesConfigs;
            for (int i = 0; i < pages.Count; i++)
            {
                var newLink = Instantiate(_pageLinkPrefab);
                newLink.transform.SetParent(_container, false);

                newLink.Init(i, pages[i].Title);
                newLink.OnFollow.AddListener((idx) => OpenPage(idx));
            }

            _themeProvider.Customize(gameObject);
        }

        public override void Open()
        {
            _pauseProvider.StopGame();

            SetScale(_view.transform, 1f, _openDuration, _openEase);
            SetTransparency(_fade, _fadeValue, _openDuration, _fadeTransparencyEase);
        }

        public override void Close()
        {
            _pauseProvider.ContinueGame();

            SetTransparency(_fade, 0f, _closeDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 0f, _closeDuration, _closeEase)
                .OnComplete(() => Destroy());
        }

        private void OpenPage(int idx)
        {
            FindObjectOfType<TheoryUI>().SwitchPageByIndex(idx);
            Close();
        }

        public class Factory : PopUpFactory<TheoryTableOfContentsPopUp>
        {
            public TheoryTableOfContentsPopUp Create(TheoryLevelConfigs configs)
            {
                var popUp = base.Create();
                popUp.Init(configs);

                return popUp;
            }
        }
    }
}