using Configs;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class LevelButtonsLayout : MonoBehaviour
    {
        private RectTransform _container;
        private float _containerAdditionalHeight;

        private Vector2 _startPosition;
        private float _spacing;
        private float _frequence;
        private float _amplitude;

        [Space]

        private float _scrollingDuration;
        private Ease _scrollingEase;

        private Tweener _scrollingTween;

        public LevelButtonsLayout(LevelButtonsConfigs configs, RectTransform container)
        {
            _container = container;
            _containerAdditionalHeight = configs.ContainerAdditionalHeight;
            _startPosition = configs.StartPosition;
            _spacing = configs.Spacing;
            _frequence = configs.Frequence;
            _amplitude = configs.Amplitude;
            _scrollingDuration = configs.ScrollingDuration;
            _scrollingEase = configs.ScrollingEase;
        }

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