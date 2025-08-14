using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TemplateSentences : MonoBehaviour
    {
        [SerializeField] private TemplateSentence _sentancePrefab;
        [SerializeField] private Transform _sentencesContainer;

        [Space]

        [SerializeField] private float _hopScale;
        [SerializeField] private float _hopDuration;
        [SerializeField] private Ease _hopEase;

        private List<TemplateSentence> _sentences = new();
        private int _currentSentenceIndex;

        public void CreateSentences(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newSentence = Instantiate(_sentancePrefab);
                newSentence.transform.SetParent(_sentencesContainer, false);
                _sentences.Add(newSentence);
            }
        }

        public void ShowNewSentence(Template.TemplateSentence sentence)
        {
            if (_currentSentenceIndex >= _sentences.Count) return;

            var view = _sentences[_currentSentenceIndex];
            view.ShowNewSentence(sentence.SentenceEn, sentence.SentenceRu);
            
            _currentSentenceIndex += 1;
        }
    }
}