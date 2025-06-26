using DG.Tweening;
using UnityEngine;

namespace Collection
{
    public class CollectionItem : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private SpriteRenderer _view;
        [SerializeField] private SpriteRenderer _shadow;
        [SerializeField] private float _initialY;

        [Space]

        [SerializeField] private float _viewAppearDuration;
        [SerializeField] private Ease _viewAppearEase;

        [SerializeField] private float _fadeDuration;
        [SerializeField] private Ease _fadeEase;

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
            if (!unlocked)
                _view.color = Color.black;

            _view.DOFade(1, _fadeDuration).SetEase(_fadeEase);
            _view.transform.DOLocalMoveY(0, _viewAppearDuration)
                .SetEase(_viewAppearEase)
                .OnComplete(() =>
                {
                    _shadow.DOFade(1, _fadeDuration).SetEase(_fadeEase);
                });
        }
    }
}