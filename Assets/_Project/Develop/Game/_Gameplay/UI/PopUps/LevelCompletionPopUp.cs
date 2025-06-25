using Configs;
using DG.Tweening;
using Theme;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class LevelCompletionPopUp : PopUp
    {
        [Space]

        [SerializeField] private Image _fade;
        [SerializeField] private float _fadeValue;
        [SerializeField] private Ease _fadeTransparencyEase;

        [Space]

        [SerializeField] private Image _itemTarget;
        [SerializeField] private Image _itemView;
        [SerializeField] private Image _itemBg;
        [SerializeField] private float _itemFillingDuration;
        [SerializeField] private Ease _itemFillingEase;
        [SerializeField] private GameObject _newItemUnlockView;
        [SerializeField] private GameObject _allItemsUnlockedView;

        [Space]

        [SerializeField] private float _itemHopScale;
        [SerializeField] private float _itemHopDuration;
        [SerializeField] private Ease _itemHopEase;

        [Space]

        [SerializeField] private Transform _lightView;
        [SerializeField] private float _lightRotationSpeed;

        [Space]

        [SerializeField] private Button _nextButton;

        private CollectionItemConfigs _itemConfigs;
        private float _currentFill;

        private GameplayUI _gameplayUI;

        [Inject]
        private void Construct(GameplayUI gameplayUI)
        {
            _gameplayUI = gameplayUI;
        }

        private new void Awake()
        {
            base.Awake();

            SetTransparency(_fade, 0f, 0, 0);
            SetScale(_view.transform, 0f, 0, 0);
        }

        private void Update()
        {
            if (_lightView.gameObject.activeSelf)
                _lightView.Rotate(-Vector3.forward * _lightRotationSpeed * Time.deltaTime);
        }

        public override void Open()
        {
            _currentFill = _gameState.State.CurrentCollectionItemFill;
            _itemConfigs = _gameConfigs.CollectionConfigs.GetUncollectedItem(
                _gameState.State.CollectedCollectionItems);

            if (_itemConfigs == null)
            {
                _allItemsUnlockedView.SetActive(true);
                _itemBg.gameObject.SetActive(false);
                _itemView.gameObject.SetActive(false);
            }
            else
            {
                _itemBg.sprite = _itemConfigs.FilledSprite;
                _itemView.sprite = _itemConfigs.FilledSprite;
                _itemBg.SetNativeSize();
                _itemView.SetNativeSize();
                _itemView.fillAmount = _currentFill;
                _allItemsUnlockedView.SetActive(false);
                _nextButton.enabled = false;

                DOVirtual.DelayedCall(_openDuration, () => FillItem(_itemConfigs.FillRate));
                DOVirtual.DelayedCall(_openDuration * 2f, () => _nextButton.enabled = true);
            }

            _lightView.gameObject.SetActive(false);
            _newItemUnlockView.SetActive(false);
            _itemTarget.raycastTarget = false;

            SetTransparency(_fade, _fadeValue, _openDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 1f, _openDuration, _openEase);
        }

        public override void Close()
        {
            SetTransparency(_fade, 0f, _closeDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 0f, _openDuration, _openEase)
                .OnComplete(() => Destroy());
        }

        public void OpenCollection()
        {
            _gameplayUI.OpenCollection();
        }

        private void FillItem(float fillRate)
        {
            var fill = _currentFill + fillRate;
            fill = Mathf.Clamp01(fill);

            if (fill == 1)
            {
                _gameState.AddCollectionItem(_itemConfigs.Id);
                _gameState.SetCurrentCollectionItemFill(0);
            }
            else
            {
                _gameState.SetCurrentCollectionItemFill(fill);
            }

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
                    _itemView.sprite = _itemConfigs.Sprite;
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
                    hopSequence.Append(
                        DOVirtual.DelayedCall(0.25f, () => _itemTarget.raycastTarget = true));
                }
            });
        }

        public class Factory : PopUpFactory<LevelCompletionPopUp>
        {
        }
    }
}