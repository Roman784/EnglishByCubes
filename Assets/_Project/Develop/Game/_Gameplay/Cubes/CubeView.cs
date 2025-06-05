using DG.Tweening;
using DG.Tweening.Core;
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
        [SerializeField] private Renderer _renderer;

        private int _curretSideIndex;
        private CubeSide _previousSide;

        private Tweener _movingTweener;
        private Tweener _rescalingTweener;

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

        public void Enable()
        {
            _collider.enabled = true;
        }

        public void Disable()
        {
            _collider.enabled = false;
        }

        public void Activate()
        {
            _view.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _view.gameObject.SetActive(false);
        }

        public void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        public Observable<bool> SetViewScale(Vector3 scale, 
                                             float duration, Ease ease, 
                                             bool completePreviousTween = false)
        {
            var onCompleted = new Subject<bool>();

            _rescalingTweener?.Kill(completePreviousTween);
            _rescalingTweener = _view.DOScale(scale, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        /*public Observable<bool> Enable(float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            _collider.enabled = true;
            _rescalingTweener?.Kill(true);
            _view.gameObject.SetActive(true);
            _rescalingTweener = _view.DOScale(Vector3.one, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        public Observable<bool> Disable(float duration, Ease ease, bool instantly)
        {
            _collider.enabled = false;

            if (instantly)
            {
                _view.localScale = Vector3.zero;
                _view.gameObject.SetActive(false);
                return Observable.Return(true);
            }

            var onCompleted = new Subject<bool>();

            _rescalingTweener?.Kill(true);
            _rescalingTweener = _view.DOScale(Vector3.zero, duration)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    _view.gameObject.SetActive(false);
                    onCompleted.OnNext(true);
                });

            return onCompleted;
        }*/
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Observable<bool> SetPosition(Vector3 position, float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            _movingTweener?.Kill(true);
            _movingTweener = transform.DOMove(position, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        public void Rotate(string word, 
                           float rotationDuration, Ease rotationEase, 
                           float fadeDuration, Ease fadeEase)
        {
            _curretSideIndex = ++_curretSideIndex % _sides.Count;

            var side = _sides[_curretSideIndex];
            var rotation = side.Rotation;
            
            side.SetWord(word);
            _view.DORotate(rotation, rotationDuration, RotateMode.Fast).SetEase(rotationEase);
            side.View.DOFade(1, fadeDuration).SetEase(fadeEase);
            _previousSide.View.DOFade(0, fadeDuration).SetEase(fadeEase);

            _previousSide = side;
        }

        public Observable<bool> Destroy(float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            SetViewScale(Vector3.zero, duration, ease).Subscribe(_ =>
            {
                Destroy(gameObject);
                onCompleted.OnNext(true);
            });

            return onCompleted;
        }
    }
}