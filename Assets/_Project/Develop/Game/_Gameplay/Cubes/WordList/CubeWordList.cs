using DG.Tweening;
using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay
{
    public class CubeWordList : MonoBehaviour
    {
        [SerializeField] private RectTransform _view;
        [SerializeField] private Transform _wordsContainer;
        [SerializeField] private CubeWord _cubeWordPrefab;

        [Space]

        [SerializeField] private float _maxViewHeight;
        [SerializeField] private int _maxCubesCount;
        
        private Tweener _wordListRescaleTweener;

        public UnityEvent<string> OnWordSelection { get; private set; } = new();

        private void Awake()
        {
            _view.localScale = Vector3.right;
            _view.rotation = Camera.main.transform.rotation;
        }

        public void CreateWords(List<string> words)
        {
            ResizeView(words.Count);
            ClearContainer(_wordsContainer);

            foreach (string word in words)
            {
                var newCubeWord = Instantiate(_cubeWordPrefab);
                newCubeWord.transform.SetParent(_wordsContainer, false);

                newCubeWord.Init(this, word);
            }
        }

        public void SelectWord(string word)
        {
            OnWordSelection.Invoke(word);
        }

        public Observable<bool> Open(float duration, Ease ease)
        {
            _view.gameObject.SetActive(true);
            return SetScale(Vector2.one, duration, ease);
        }

        public Observable<bool> Close(float duration, Ease ease)
        {
            var res = SetScale(Vector2.right, duration, ease);
            res.Subscribe(_ => _view.gameObject.SetActive(false));

            return res;
        }

        private Observable<bool> SetScale(Vector2 scale, float duration, Ease ease)
        {
            var onCompleted = new Subject<bool>();

            _wordListRescaleTweener?.Kill(false);
            _wordListRescaleTweener = _view.DOScale(scale, duration)
                .SetEase(ease)
                .OnComplete(() => onCompleted.OnNext(true));

            return onCompleted;
        }

        private void ResizeView(int wordsCount)
        {
            var cubeWordHeight = _maxViewHeight / _maxCubesCount;
            var viewHeight = wordsCount * cubeWordHeight;
            viewHeight = Mathf.Clamp(viewHeight, 0, _maxViewHeight);

            _view.sizeDelta = new Vector2(_view.rect.width, viewHeight);
        }

        private void ClearContainer(Transform container)
        {
            var childCount = container.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }
    }
}