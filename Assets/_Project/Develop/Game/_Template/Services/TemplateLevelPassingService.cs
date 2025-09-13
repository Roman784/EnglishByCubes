using Gameplay;
using GameRoot;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Template
{
    public class TemplateLevelPassingService : ILevelPassingService
    {
        private List<TemplateSentence> _targetSentances = new();
        private List<string> _banWords = new();
        private int _countToPass;

        public UnityEvent<TemplateSentence, int> OnNewSentenceFounded { get; private set; } = new();

        public void SetTargetSentences(List<TemplateSentence> sentences, int countToPass)
        {
            _targetSentances = new(sentences);
            _countToPass = countToPass;
        }

        public void CalculateSentenceMatching(List<string> words)
        {
            if (IsInBanList(words)) return;

            string player = MakeSentence(words).ToLower().Replace(" ", "").Trim();

            foreach (var sentence in _targetSentances)
            {
                string correct = sentence.TargetSentence.ToLower().Replace(" ", "").Trim();
                if (!player.Equals(correct)) continue;

                AddInBanList(words);
                _targetSentances.Remove(sentence);
                _countToPass -= 1;
                OnNewSentenceFounded.Invoke(sentence, _countToPass);

                break;
            }
        }

        private bool IsInBanList(List<string> words)
        {
            foreach (var word in words)
            {
                if (_banWords.Contains(word))
                    return true;
            }
            return false;
        }

        private void AddInBanList(List<string> words)
        {
            _banWords.AddRange(words);
        }

        public void CalculateSentenceMatching(string playerSentence) { }

        private string MakeSentence(List<string> words)
        {
            var sentence = "";
            foreach (var word in words)
                sentence += word;

            return sentence;
        }
    }
}