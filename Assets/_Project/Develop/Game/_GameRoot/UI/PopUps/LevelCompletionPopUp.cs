using Configs;
using DG.Tweening;
using GameRoot;
using System;
using Theme;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
        [SerializeField] private TMP_Text _percentageView;

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
            _pauseProvider.StopGame();

            _currentFill = _gameState.State.CurrentCollectionItemFill;
            _itemConfigs = _gameConfigs.CollectionConfigs.GetUncollectedItem(
                _gameState.State.CollectedCollectionItems);

            if (_itemConfigs == null)
            {
                _allItemsUnlockedView.SetActive(true);
                _itemBg.gameObject.SetActive(false);
                _itemView.gameObject.SetActive(false);
                _percentageView.gameObject.SetActive(false);
            }
            else
            {
                _itemBg.sprite = _itemConfigs.FilledSprite;
                _itemView.sprite = _itemConfigs.FilledSprite;
                _itemBg.SetNativeSize();
                _itemView.SetNativeSize();
                _itemView.fillAmount = _currentFill;
                _allItemsUnlockedView.SetActive(false);
                _percentageView.gameObject.SetActive(true);
                _percentageView.text = $"{_currentFill * 100:F0}%";
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
            _pauseProvider.ContinueGame();

            SetTransparency(_fade, 0f, _closeDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 0f, _closeDuration, _closeEase)
                .OnComplete(() => Destroy());
        }

        public void OpenCollection()
        {
            if (SceneManager.GetActiveScene().name == Scenes.GAMEPLAY)
            {
                var ui = FindObjectOfType<GameplayUI>();
                ui.OpenCollection();
            }
            else if (SceneManager.GetActiveScene().name == Scenes.TEMPLATE)
            {
                var ui = FindObjectOfType<TemplateUI>();
                ui.OpenCollection();
            }
        }

        public void OpenNextLevel()
        {
            if (SceneManager.GetActiveScene().name == Scenes.GAMEPLAY)
            {
                var ui = FindObjectOfType<GameplayUI>();
                ui.OpenNextLevel();
            }
            else if (SceneManager.GetActiveScene().name == Scenes.TEMPLATE)
            {
                var ui = FindObjectOfType<TemplateUI>();
                ui.OpenNextLevel();
            }
        }

        private void FillItem(float fillRate)
        {
            UnityEvent stopSoundEvent = new();
            PlayFillingSound(stopSoundEvent);

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
                    _percentageView.text = $"{_currentFill * 100:F0}%";
                },
                fill,
                _itemFillingDuration
            )
            .SetEase(_itemFillingEase)
            .OnComplete(() =>
            {
                stopSoundEvent.Invoke();

                if (_currentFill == 1)
                {
                    PlayNewItemUnlocked();

                    _itemView.sprite = _itemConfigs.Sprite;
                    _lightView.gameObject.SetActive(true);
                    _newItemUnlockView.SetActive(true);
                    _percentageView.gameObject.SetActive(false);

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

        private void PlayFillingSound(UnityEvent stopEvent)
        {
            var clip = AudioConfigs.CollectionConfigs.ItemFillingSound;
            _audioProvider.PlaySound(clip, stopEvent);
        }

        private void PlayNewItemUnlocked()
        {
            var clip = AudioConfigs.CollectionConfigs.NewItemUnlockSound;
            _audioProvider.PlaySound(clip);
        }

        public class Factory : PopUpFactory<LevelCompletionPopUp>
        {
        }
    }
}