using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fillerView;
        [SerializeField] private TMP_Text _percentageView;
        [SerializeField] private RectTransform _background;

        private float _currentFill;
        private Tweener _fillingTween;

        private ProgressBarConfigs _configs;

        public void Init(ProgressBarConfigs configs)
        {
            _configs = configs;
            SetView(0);
        }

        public void Fill(float fill)
        {
            fill = Mathf.Clamp01(fill);

            _fillingTween?.Kill(false);
            _fillingTween = DOTween.To(
                () => _currentFill,
                x =>
                {
                    _currentFill = x;
                    SetView(_currentFill);
                },
                fill,
                _configs.FillingDuration
            ).SetEase(_configs.FillingEase);
        }

        private void SetView(float fill)
        {
            RescaleFillerView(fill);
            _fillerView.color = _configs.FillingGradient.Evaluate(fill);
            _percentageView.text = $"{fill * 100:F0}%";
        }

        private void RescaleFillerView(float fill)
        {
            _fillerView.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Horizontal,
                _background.rect.width * fill
            );
        }
    }
}