using Configs;
using DG.Tweening;
using R3;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _view;

        private IConfigsProvider _configsProvider;
        private LoadingScreenConfigs Configs => _configsProvider.GameConfigs.UIConfigs.LoadingScreenConfigs;

        [Inject]
        private void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        private void Awake()
        {
            _view.gameObject.SetActive(true);
            _view.alpha = 1f;
        }

        public Observable<Unit> Show()
        {
            _view.gameObject.SetActive(true);
            return SetViewAlpha(0, 1, Configs.ShowingDuration, Configs.ShowingEase);
        }

        public Observable<Unit> Hide()
        {
            var onCompleted = SetViewAlpha(1, 0, Configs.HidingDuration, Configs.HidingEase);
            onCompleted.Subscribe(_ => _view.gameObject.SetActive(false));

            return onCompleted;
        }

        private Observable<Unit> SetViewAlpha(float from, float to, float duration, Ease ease)
        {
            var onCompleted = new Subject<Unit>();

            _view.alpha = from;
            _view.DOFade(to, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(Unit.Default));

            return onCompleted;
        }
    }
}