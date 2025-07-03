using Audio;
using Configs;
using DG.Tweening;
using UI;
using UnityEngine;
using Zenject;

namespace Collection
{
    public class CollectionItem : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private SpriteRenderer _view;
        [SerializeField] private SpriteRenderer _shadow;
        [SerializeField] private float _initialY;
        [SerializeField] private GameObject _raycastTarget;

        [Space]

        [SerializeField] private float _viewAppearDuration;
        [SerializeField] private Ease _viewAppearEase;

        [SerializeField] private float _fadeDuration;
        [SerializeField] private Ease _fadeEase;

        private bool _isUnlocked;

        private AudioProvider _audioProvider; 
        private IConfigsProvider _configsProvider;

        public int Id => _id;

        [Inject]
        private void Construct(AudioProvider audioProvider, IConfigsProvider configsProvider)
        {
            _audioProvider = audioProvider;
            _configsProvider = configsProvider;
        }

        private void Awake()
        {
            var color = Color.white;
            color.a = 0f;

            _view.transform.localPosition = Vector2.up * _initialY;
            _view.color = color;
            _shadow.color = color;
        }

        private void OnDestroy()
        {
            DOTween.Kill(_view);
        }

        public void Appear(bool unlocked)
        {
            _isUnlocked = unlocked;
            if (!unlocked)
                _view.color = Color.black;

            _raycastTarget.SetActive(_isUnlocked);

            _view.DOFade(1, _fadeDuration).SetEase(_fadeEase);
            _view.transform.DOLocalMoveY(0, _viewAppearDuration)
                .SetEase(_viewAppearEase)
                .OnComplete(() =>
                {
                    _shadow.DOFade(1, _fadeDuration).SetEase(_fadeEase);
                });

            DOVirtual.DelayedCall(_viewAppearDuration / 3.5f, PlayFallSound);
        }

        public void OpenContent()
        {
            if (!_isUnlocked) return;

            var ui = FindObjectOfType<CollectionUI>();
            ui?.OpenItemContent(Id);
        }

        private void PlayFallSound()
        {
            var clip = _configsProvider.GameConfigs.AudioConfigs.CollectionConfigs.ItemFallSound;
            _audioProvider.PlaySound(clip);
        }
    }
}