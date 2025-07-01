using GameRoot;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Template
{
    public class TemplateLevelPassingService : ILevelPassingService
    {
        private List<TemplateSentence> _targetSentances = new();

        public UnityEvent<TemplateSentence, int> OnNewSentenceFounded { get; private set; } = new();

        public void SetTargetSentences(List<TemplateSentence> sentences)
        {
            _targetSentances = new(sentences);
        }

        public void CalculateSentenceMatching(string playerSentence)
        {
            string player = playerSentence.ToLower().Replace(" ", "").Trim();

            foreach (var sentence in _targetSentances)
            {
                string correct = sentence.TargetSentence.ToLower().Replace(" ", "").Trim();
                if (!player.Equals(correct)) continue;

                _targetSentances.Remove(sentence);
                OnNewSentenceFounded.Invoke(sentence, _targetSentances.Count);

                break;
            }
        }
    }
}