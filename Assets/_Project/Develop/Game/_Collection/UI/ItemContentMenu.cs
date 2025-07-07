using Configs;
using DG.Tweening;
using Pause;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ItemContentMenu : MonoBehaviour
    {
        [SerializeField] private Transform _view;
        [SerializeField] private Image _pictureView;
        [SerializeField] private TMP_Text _titleView;
        [SerializeField] private TMP_Text _mainTextView;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

        [Space]

        [SerializeField] private Image _fadeView;
        [SerializeField] private float _fadeValue;
        [SerializeField] private Ease _fadeTransparencyEase;

        [Space]

        [SerializeField] private float _openDuration;
        [SerializeField] private Ease _openEase;
        [SerializeField] private float _closeDuration;
        [SerializeField] private Ease _closeEase;

        private Tweener _transparencyTween;
        private Tweener _scaleTween;

        private PauseProvider _pauseProvider;

        [Inject]
        private void Construct(PauseProvider pauseProvider)
        {
            _pauseProvider = pauseProvider;
        }

        private void Awake()
        {
            SetViewScale(0f, 0f, 0);
            SetFadeTransparency(0f, 0f, 0f);

            _view.gameObject.SetActive(false);
            _fadeView.gameObject.SetActive(false);
        }

        public void Open(CollectionItemConfigs configs)
        {
            _pauseProvider.StopGame();

            SetView(configs);

            _view.gameObject.SetActive(true);
            SetViewScale(1f, _openDuration, _openEase);
            _fadeView.gameObject.SetActive(true);
            SetFadeTransparency(_fadeValue, _openDuration, _openEase);
        }

        public void Close()
        {
            _pauseProvider.ContinueGame();

            SetViewScale(0f, _closeDuration, _closeEase)
                .OnComplete(() => _view.gameObject.SetActive(false));
            SetFadeTransparency(0f, _closeDuration, _closeEase)
                .OnComplete(() => _fadeView.gameObject.SetActive(false));
        }

        private void SetView(CollectionItemConfigs configs)
        {
            _pictureView.sprite = configs.Picture;
            _titleView.text = configs.Title;
            _mainTextView.text = configs.MainText;

            _verticalLayoutGroup.CalculateLayoutInputVertical();
            _verticalLayoutGroup.SetLayoutVertical();

            _verticalLayoutGroup.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        private Tweener SetFadeTransparency(float value, float duration, Ease ease)
        {
            _transparencyTween?.Kill(false);
            _transparencyTween = _fadeView.DOFade(value, duration).SetEase(ease);

            return _transparencyTween;
        }

        private Tweener SetViewScale(float value, float duration, Ease ease)
        {
            _scaleTween?.Kill(true);
            _scaleTween = _view.DOScale(value, duration).SetEase(ease);

            return _scaleTween;
        }
    }
}