using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DotsProgressBar : MonoBehaviour
    {
        [SerializeField] private ProgressBarDot _dotPrefab;
        [SerializeField] private Transform _dotsContainer;

        private List<ProgressBarDot> _dots = new();
        private ProgressBarDot _currentDot;

        public void CreateDots(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newDot = Instantiate(_dotPrefab);
                newDot.transform.SetParent(_dotsContainer, false);

                _dots.Add(newDot);
            }
        }

        public void HighlightDot(int index)
        {
            _currentDot?.RemoveHighlight();

            if (index < 0 || index >= _dots.Count) return;

            _currentDot = _dots[index];
            _currentDot.Highlight();
        }
    }
}