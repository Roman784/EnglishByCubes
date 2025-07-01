using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelInfoPopUp : PopUp
    {
        [Space]

        [SerializeField] private Image _fade;
        [SerializeField] private float _fadeValue;
        [SerializeField] private Ease _fadeTransparencyEase;

        [Space]

        [SerializeField] private RectTransform _contentContainer;

        private new void Awake()
        {
            SetTransparency(_fade, 0f, 0, 0);
            SetScale(_view.transform, 0f, 0, 0);
        }

        public void Init(GameObject contentPrefab)
        {
            var content = Instantiate(contentPrefab);
            content.transform.SetParent(_contentContainer, false);

            _themeProvider.Customize(gameObject);
        }

        public override void Open()
        {
            SetScale(_view.transform, 1f, _openDuration, _openEase);
            SetTransparency(_fade, _fadeValue, _openDuration, _fadeTransparencyEase);
        }

        public override void Close()
        {
            SetTransparency(_fade, 0f, _closeDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 0f, _closeDuration, _closeEase)
                .OnComplete(() => Destroy());
        }

        public class Factory : PopUpFactory<LevelInfoPopUp>
        {
            public LevelInfoPopUp Create(GameObject contentPrefab)
            {
                var popUp = base.Create();
                popUp.Init(contentPrefab);

                return popUp;
            }
        }
    }
}