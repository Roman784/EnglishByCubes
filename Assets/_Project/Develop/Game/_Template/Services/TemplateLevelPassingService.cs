using GameRoot;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Template
{
    public class TemplateLevelPassingService : ILevelPassingService
    {
        private List<TemplateSentence> _targetSentances = new();
        private int _countToPass;

        public UnityEvent<TemplateSentence, int> OnNewSentenceFounded { get; private set; } = new();

        public void SetTargetSentences(List<TemplateSentence> sentences, int countToPass)
        {
            _targetSentances = new(sentences);
            _countToPass = countToPass;
        }

        public void CalculateSentenceMatching(string playerSentence)
        {
            string player = playerSentence.ToLower().Replace(" ", "").Trim();

            foreach (var sentence in _targetSentances)
            {
                string correct = sentence.TargetSentence.ToLower().Replace(" ", "").Trim();
                if (!player.Equals(correct)) continue;

                _targetSentances.Remove(sentence);
                _countToPass -= 1;
                OnNewSentenceFounded.Invoke(sentence, _countToPass);

                break;
            }
        }
    }
}