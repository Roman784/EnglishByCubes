using DG.Tweening;
using R3;
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
        [SerializeField] private Collider _collider;

        [Space]

        [SerializeField] private float _rotationDuration;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _rescaleDuration;
        [SerializeField] private Ease _rotationEase;
        [SerializeField] private Ease _fadeEase;
        [SerializeField] private Ease _rescaleEase;

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

        public Observable<bool> Enable()
        {
            var onCompleted = new Subject<bool>();

            _collider.enabled = true;
            _view.gameObject.SetActive(true);
            _view.DOScale(Vector3.one, _rescaleDuration)
                .SetEase(_rescaleEase)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        public Observable<bool> Disable(bool instantly)
        {
            _collider.enabled = false;

            if (instantly)
            {
                _view.gameObject.SetActive(false);
                return Observable.Return(true);
            }

            var onCompleted = new Subject<bool>();

            _view.DOScale(Vector3.zero, _rescaleDuration)
                .SetEase(_rescaleEase)
                .OnComplete(() =>
                {
                    _view.gameObject.SetActive(false);
                    onCompleted.OnNext(true);
                });

            return onCompleted;
        }

        public Observable<bool> SetPosition(Vector3 position, float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            transform.DOMove(position, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        public void SetScale(float scale)
        {
            _view.localScale = Vector3.one * scale;
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