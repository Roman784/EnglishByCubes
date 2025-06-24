using Configs;
using DG.Tweening;
using GameState;
using Theme;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public abstract class PopUp : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _view;

        [Space]

        [SerializeField] protected float _openDuration;
        [SerializeField] protected Ease _openEase;
        [SerializeField] protected float _closeDuration;
        [SerializeField] protected Ease _closeEase;

        protected GameConfigs _gameConfigs;
        protected GameStateProxy _gameState;
        protected ThemeProvider _themeProvider;

        private Tweener _transparencyTween;
        private Tweener _scaleTween;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, IGameStateProvider gameStateProvider, ThemeProvider themeProvider)
        {
            _gameConfigs = configsProvider.GameConfigs;
            _gameState = gameStateProvider.GameStateProxy;
            _themeProvider = themeProvider;
        }

        protected void Awake()
        {
            _themeProvider.Customize(gameObject);
        }

        public virtual void Open()
        {
            SetTransparency(_view, 1, _openDuration, _openEase);
        }

        public virtual void Close()
        {
            SetTransparency(_view, 0, _closeDuration, _closeEase)
                .OnComplete(() => Destroy());
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        protected Tweener SetTransparency(Image view, float value, float duration, Ease ease)
        {
            _transparencyTween?.Kill(true);
            _transparencyTween = view.DOFade(value, duration).SetEase(ease);

            return _transparencyTween;
        }

        protected Tweener SetTransparency(CanvasGroup view, float value, float duration, Ease ease)
        {
            _transparencyTween?.Kill(true);
            _transparencyTween = view.DOFade(value, duration).SetEase(ease);

            return _transparencyTween;
        }

        protected Tweener SetScale(Transform target, float value, float duration, Ease ease)
        {
            _scaleTween?.Kill(true);
            _scaleTween = target.DOScale(value, duration).SetEase(ease);

            return _scaleTween;
        }
    }
}