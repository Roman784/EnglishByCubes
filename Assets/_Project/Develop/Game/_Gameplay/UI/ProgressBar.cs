using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fillerView;
        [SerializeField] private TMP_Text _percentageView;
        [SerializeField] private RectTransform _background;

        [Space]

        [SerializeField] private float _fillingDuration;
        [SerializeField] private Ease _fillingEase;
        [SerializeField] private Gradient _fillingGradient;

        private float _currentFill;
        private Coroutine _fillingRoutine;
        private Tweener _fillingTween;

        private void Awake()
        {
            SetView(0);
        }

        public void Fill(float currentValue, float maxValue)
        {
            var fill = currentValue / maxValue;
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
                _fillingDuration
            ).SetEase(_fillingEase);

            /*if (_fillingRoutine != null)
                Coroutines.Stop(_fillingRoutine);
            _fillingRoutine = Coroutines.Start(FillRoutine(fill));*/
        }

        /*private IEnumerator FillRoutine(float fill)
        {
            while (_currentFill != fill)
            {
                _currentFill = Mathf.Lerp(_currentFill, fill, _fillingRate * Time.deltaTime);
                SetView(_currentFill);

                yield return null;
            }
        }*/

        private void SetView(float fill)
        {
            RescaleFillerView(fill);
            _fillerView.color = _fillingGradient.Evaluate(fill);
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