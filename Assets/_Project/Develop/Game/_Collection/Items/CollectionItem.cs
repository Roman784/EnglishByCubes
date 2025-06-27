using DG.Tweening;
using UI;
using UnityEngine;

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

        public int Id => _id;

        private void Awake()
        {
            var color = Color.white;
            color.a = 0f;

            _view.transform.localPosition = Vector2.up * _initialY;
            _view.color = color;
            _shadow.color = color;
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
        }

        public void OpenContent()
        {
            if (!_isUnlocked) return;

            var ui = FindObjectOfType<CollectionUI>();
            ui?.OpenItemContent(Id);
        }
    }
}