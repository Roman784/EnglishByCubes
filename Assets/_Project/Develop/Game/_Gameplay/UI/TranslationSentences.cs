using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TranslationSentences : MonoBehaviour
    {
        [SerializeField] private TranslationSentence _sentencePrefab;
        [SerializeField] private Transform _sentencesContainer;

        private List<TranslationSentence> _enSentences = new();

        public void CreateSentences(List<SentenceConfigs> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var newSentence = Instantiate(_sentencePrefab);
                newSentence.transform.SetParent(_sentencesContainer, false);

                newSentence.SetRu(data[i].SentenceRu);
                newSentence.SetEn(data[i].SentenceEn);

                _enSentences.Add(newSentence);
            }
        }

        public void ShowTranslation(int sentenceIdx)
        {
            _enSentences[sentenceIdx].Show();
        }
    }
}