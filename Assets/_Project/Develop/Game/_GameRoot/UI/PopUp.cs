using Audio;
using Configs;
using DG.Tweening;
using GameRoot;
using GameState;
using Pause;
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
        protected PopUpsRoot _root;
        protected ThemeProvider _themeProvider;
        protected PauseProvider _pauseProvider;
        protected AudioProvider _audioProvider;
        protected SceneProvider _sceneProvider;
        protected PopUpsProvider _popUpsProvider;

        private Tweener _transparencyTween;
        private Tweener _scaleTween;

        protected AudioConfigs AudioConfigs => _gameConfigs.AudioConfigs;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, IGameStateProvider gameStateProvider,
                               UIRoot uiRoot, ThemeProvider themeProvider, PauseProvider pauseProvider,
                               AudioProvider audioProvider, SceneProvider sceneProvider, PopUpsProvider popUpsProvider)
        {
            _gameConfigs = configsProvider.GameConfigs;
            _gameState = gameStateProvider.GameStateProxy;
            _root = uiRoot.PopUpsRoot;
            _themeProvider = themeProvider;
            _pauseProvider = pauseProvider;
            _audioProvider = audioProvider;
            _sceneProvider = sceneProvider;
            _popUpsProvider = popUpsProvider;
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
            _root.RemovePopUp(this);
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