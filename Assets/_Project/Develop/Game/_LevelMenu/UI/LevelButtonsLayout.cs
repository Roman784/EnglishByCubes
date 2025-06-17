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
    }
}