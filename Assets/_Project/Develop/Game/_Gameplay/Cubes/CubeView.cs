using DG.Tweening;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
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
        [SerializeField] private float _fadeDuration;
        [SerializeField] private Ease _rotationEase;
        [SerializeField] private Ease _fadeEase;

        [Space]

        [SerializeField] private Renderer _renderer;

        private int _curretSideIndex;
        private CubeSide _previousSide;

        public UnityEvent OnPressed { get; private set; } = new();
        public UnityEvent OnUnpressed { get; private set; } = new();

        private void OnMouseDown() => OnPressed.Invoke();
        private void OnMouseUp() => OnUnpressed.Invoke();

        public void Init(string intialWord, Material material)
        {
            foreach (var side in _sides)
                side.View.DOFade(0, 0);
            _sides[0].View.DOFade(1, 0);

            _previousSide = _sides[0];
            _sides[0].SetWord(intialWord);

            _renderer.material = material;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Rotate(string word)
        {
            _curretSideIndex = ++_curretSideIndex % _sides.Count;

            var side = _sides[_curretSideIndex];
            var rotation = side.Rotation;
            
            side.SetWord(word);
            _view.DORotate(rotation, _rotationDuration, RotateMode.Fast).SetEase(_rotationEase);
            side.View.DOFade(1, _fadeDuration).SetEase(_fadeEase);
            _previousSide.View.DOFade(0, _fadeDuration).SetEase(_fadeEase);

            _previousSide = side;
        }
    }
}