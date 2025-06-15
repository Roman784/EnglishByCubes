using Configs;
using DG.Tweening;
using Theme;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public abstract class PopUp : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _view;

        protected PopUpConfigs _config;
        protected ThemeProvider _themeProvider;

        private Tweener _transparencyTween;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, ThemeProvider themeProvider)
        {
            _config = configsProvider.GameConfigs.UIConfigs.PopUpConfigs;
            _themeProvider = themeProvider;
        }

        protected void Awake()
        {
            _view.alpha = 0f;

            _themeProvider.Customize(gameObject);
        }

        public virtual void Open()
        {
            var duration = _config.OpenDuration;
            var ease = _config.OpenEase;

            SetViewTransparency(1, duration, ease);
        }

        public virtual void Close()
        {
            var duration = _config.CloseDuration;
            var ease = _config.CloseEase;

            SetViewTransparency(0, duration, ease)
                .OnComplete(() => Destroy());
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private Tweener SetViewTransparency(float value, float duration, Ease ease)
        {
            _transparencyTween?.Kill(true);
            _transparencyTween = _view.DOFade(value, duration).SetEase(ease);

            return _transparencyTween;
        }
    }
}