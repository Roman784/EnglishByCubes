using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TemplateSentence : MonoBehaviour
    {
        [SerializeField] private TMP_Text _view;

        [Space]

        [SerializeField] private float _hopScale;
        [SerializeField] private float _hopDuration;
        [SerializeField] private Ease _hopEase;

        public void ShowNewSentence(string sentenceEn, string sentenceRu)
        {
            var hopSequence = DOTween.Sequence();
            var originScale = _view.transform.localScale;

            hopSequence.Append(
                _view.transform.DOScale(originScale * _hopScale, _hopDuration)
                .SetEase(_hopEase).OnComplete(() =>
                {
                    var text = $"{sentenceEn}\n{sentenceRu}";

                    _view.alignment = TextAlignmentOptions.Left;
                    _view.text = text;
                }));
            hopSequence.Append(
                _view.transform.DOScale(1, _hopDuration)
                .SetEase(_hopEase));
        }
    }
}