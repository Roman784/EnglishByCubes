using DG.Tweening;
using R3;
using System.Collections.Generic;
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
        [SerializeField] private CubeWordList _wordList;
        [SerializeField] private CubeName _name;

        private int _curretSideIndex;
        private CubeSide _previousSide;

        private Tweener _movementTweener;

        public UnityEvent OnPointerDown { get; private set; } = new();
        public UnityEvent OnPointerUp { get; private set; } = new();
        public UnityEvent OnPointerEnter { get; private set; } = new();
        public UnityEvent OnPointerExit { get; private set; } = new();

        public UnityEvent OnInRemoveAreaEnter { get; private set; } = new();
        public UnityEvent OnInRemoveAreaExit { get; private set; } = new();

        public UnityEvent<string> OnWordInWordListSelected => _wordList.OnWordSelection;

        private void OnMouseDown() => OnPointerDown.Invoke();
        private void OnMouseUpAsButton() => OnPointerUp.Invoke();
        private void OnMouseEnter() => OnPointerEnter.Invoke();
        private void OnMouseExit() => OnPointerExit.Invoke();

        public void Init(int number, string name, List<string> words, Material material)
        {
            if (_sides == null || _sides.Count == 0)
                throw new System.NullReferenceException("The cube must have at least 1 side!");

            foreach (var side in _sides)
                side.View.DOFade(0, 0);
            _sides[0].View.DOFade(1, 0);

            var initialWord = words.Count > 0 ? words[0] : "";
            _previousSide = _sides[0];
            _sides[0].SetWord(initialWord);

            _wordList.CreateWords(words);
            _name.SetName(number, name);

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

        public void SetViewScale(Vector3 scale)
        {
            _view.localScale = scale;
        }

        public Observable<bool> SetScale(Vector3 scale, float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            transform.DOScale(scale, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        public Observable<bool> SetViewScale(Vector3 scale, float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            _view.DOScale(scale, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Observable<bool> SetPosition(Vector3 position, float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            _movementTweener?.Kill(false);
            _movementTweener = transform.DOMove(position, duration)
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

        public Observable<bool> OpenWordList(float duration, Ease ease)
        {
            return _wordList.Open(duration, ease);
        }

        public Observable<bool> CloseWordList(float duration, Ease ease)
        {
            return _wordList.Close(duration, ease);
        }

        public void ShowName(float duration, Ease ease)
        {
            _name.Show(duration, ease);
        }

        public void HideName(float duration, Ease ease)
        {
            _name.Hide(duration, ease);
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