using GameRoot;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class GameplayLevelPassingService : ILevelPassingService
    {
        private List<string> _sentences;
        private List<int> _completedIdxs = new();

        // <sentence idx, completed sentences>
        public UnityEvent<int, int> OnSentenceMatchingCalculated { get; private set; } = new();

        public void SetCorrectSentences(List<string> sentences)
        {
            _sentences = new List<string>(sentences);
        }

        public void CalculateSentenceMatching(string playerSentence)
        {
            string player = playerSentence.ToLower().Replace(" ", "").Trim();

            for (int i = 0; i < _sentences.Count; i++)
            {
                string correct = _sentences[i].ToLower().Replace(" ", "").Trim();

                if (!correct.Equals(player) || _completedIdxs.Contains(i)) continue;

                _completedIdxs.Add(i);
                OnSentenceMatchingCalculated.Invoke(i, _completedIdxs.Count);

                break;
            }
        }
    }
}