using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Template
{
    public class TemplateSentences : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> _sentenceViews;

        [Space]

        [SerializeField] private float _hopScale;
        [SerializeField] private float _hopDuration;
        [SerializeField] private Ease _hopEase;

        private int _currentSentenceIndex;

        public void ShowNewSentence(TemplateSentence sentence)
        {
            if (_currentSentenceIndex >= _sentenceViews.Count) return;

            var view = _sentenceViews[_currentSentenceIndex];

            var hopSequence = DOTween.Sequence();
            var originScale = view.transform.localScale;

            hopSequence.Append(
                view.transform.DOScale(originScale * _hopScale, _hopDuration)
                .SetEase(_hopEase).OnComplete(() =>
                {
                    var text = $"{sentence.SentenceEn}\n{sentence.SentenceRu}";

                    view.alignment = TextAlignmentOptions.Left;
                    view.text = text;
                }));
            hopSequence.Append(
                view.transform.DOScale(1, _hopDuration)
                .SetEase(_hopEase));

            _currentSentenceIndex += 1;
        }
    }
}