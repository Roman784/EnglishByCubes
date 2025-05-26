using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private List<CubeSide> _sides;
        [SerializeField] private Transform _view;

        [Space]

        [SerializeField] private float _rotationDuration;
        [SerializeField] private float _viewFadeDuration;
        [SerializeField] private Ease _rotationEase;
        [SerializeField] private Ease _viewFadeEase;

        public UnityEvent OnPressed { get; private set; } = new();
        public UnityEvent OnUnpressed { get; private set; } = new();

        private void OnMouseDown() => OnPressed.Invoke();
        private void OnMouseUp() => OnUnpressed.Invoke();

        public void Rotate(int sideIndex)
        {
            var side = _sides[sideIndex % _sides.Count];
            var rotation = side.Rotation;

            _view.DORotate(rotation, _rotationDuration, RotateMode.Fast).SetEase(_rotationEase);
        }
    }
}