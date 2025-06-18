using Configs;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class LevelButtonsLayout : MonoBehaviour
    {
        [Header("Buttons Container")]
        [SerializeField] private RectTransform _container;
        [SerializeField] private float _containerAdditionalHeight;

        [Header("Buttons")]
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private float _spacing;
        [SerializeField] private float _frequence;
        [SerializeField] private float _amplitude;

        [Space]

        [SerializeField] private float _scrollingDuration;
        [SerializeField] private Ease _scrollingEase;

        private Tweener _scrollingTween;

        public void LayOut(RectTransform button, int index)
        {
            var xPosition = Mathf.Sin(index * Mathf.PI / _frequence) * _amplitude;
            var yPosition = index * _spacing;
            var position = new Vector2(xPosition, yPosition) + _startPosition;

            button.SetParent(_container, false);
            button.anchoredPosition = position;
        }

        public void ResizeContainer(int buttonsCount)
        {
            var totalHeight = buttonsCount * _spacing + _containerAdditionalHeight;
            _container.sizeDelta = new Vector2(_container.sizeDelta.x, totalHeight);
        }

        public void ScrollTo(int buttonIndex, bool instantly)
        {
            var minY = -(_container.rect.height - Screen.height);
            var newY = -(--buttonIndex) * _spacing;
            newY = Mathf.Clamp(newY, minY, 0);

            if (instantly)
            {
                _container.anchoredPosition = new Vector2(_container.anchoredPosition.x, newY);
                return;
            }

            _scrollingTween?.Kill(false);
            _scrollingTween = _container.DOAnchorPosY(newY, _scrollingDuration).SetEase(_scrollingEase);
        }
    }
}