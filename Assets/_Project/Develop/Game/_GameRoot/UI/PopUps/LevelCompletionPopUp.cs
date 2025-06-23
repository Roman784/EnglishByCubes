using Configs;
using DG.Tweening;
using Theme;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelCompletionPopUp : PopUp
    {
        [Space]

        [SerializeField] private Image _fade;
        [SerializeField] private float _fadeValue;
        [SerializeField] private Ease _fadeTransparencyEase;

        [Space]

        [SerializeField] private Image _itemView;
        [SerializeField] private Image _itemBg;
        [SerializeField] private float _itemFillingDuration;
        [SerializeField] private Ease _itemFillingEase;
        [SerializeField] private GameObject _newItemUnlockView;

        [Space]

        [SerializeField] private float _itemHopScale;
        [SerializeField] private float _itemHopDuration;
        [SerializeField] private Ease _itemHopEase;

        [Space]

        [SerializeField] private Transform _lightView;
        [SerializeField] private float _lightRotationSpeed;

        [Space]

        [SerializeField] private Button _nextButton;

        [Space]

        [SerializeField] private Sprite _itemSprite;
        [SerializeField] private Sprite _filledItemSprite;

        private float _currentFill = 0.3f;

        private new void Awake()
        {
            base.Awake();

            SetTransparency(_fade, 0f, 0, 0);
            SetScale(_view.transform, 0f, 0, 0);

            _itemView.fillAmount = _currentFill;
            _lightView.gameObject.SetActive(false);
            _newItemUnlockView.SetActive(false);

            _nextButton.enabled = false;

            SetInitialItemView();
        }

        private void Update()
        {
            if (_lightView.gameObject.activeSelf)
                _lightView.Rotate(-Vector3.forward * _lightRotationSpeed * Time.deltaTime);
        }

        public override void Open()
        {
            SetTransparency(_fade, _fadeValue, _openDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 1f, _openDuration, _openEase);

            DOVirtual.DelayedCall(_openDuration * 1.5f, () =>
            {
                _nextButton.enabled = true;
                FillItem();
            });
        }

        public override void Close()
        {
            SetTransparency(_fade, 0f, _closeDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 0f, _openDuration, _openEase)
                .OnComplete(() => Destroy());
        }

        private void FillItem()
        {
            var fill = 1f;
            fill = Mathf.Clamp01(fill);

            DOTween.To(
                () => _currentFill,
                x =>
                {
                    _currentFill = x;
                    _itemView.fillAmount = _currentFill;
                },
                fill,
                _itemFillingDuration
            )
            .SetEase(_itemFillingEase)
            .OnComplete(() =>
            {
                if (_currentFill == 1)
                {
                    _itemView.sprite = _itemSprite;
                    _lightView.gameObject.SetActive(true);
                    _newItemUnlockView.SetActive(true);

                    var hopSequence = DOTween.Sequence();
                    var originScale = _itemView.transform.localScale;

                    hopSequence.Append(
                        _itemView.transform.DOScale(originScale * _itemHopScale, _itemHopDuration)
                        .SetEase(_itemHopEase));
                    hopSequence.Append(
                        _itemView.transform.DOScale(originScale, _itemHopDuration)
                        .SetEase(_itemHopEase));
                }
            });
        }

        private void SetInitialItemView()
        {
            _itemBg.sprite = _filledItemSprite;
            _itemView.sprite = _filledItemSprite;
        }

        public class Factory : PopUpFactory<LevelCompletionPopUp>
        {
        }
    }
}