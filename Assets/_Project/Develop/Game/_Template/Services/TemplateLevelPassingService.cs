using Gameplay;
using GameRoot;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Template
{
    public class TemplateLevelPassingService : ILevelPassingService
    {
        private List<SentenceConfigs> _targetSentances = new();
        private List<string> _banWords = new();
        private string _lastMatchingSentence;
        private int _countToPass;

        public UnityEvent<SentenceConfigs, int> OnNewSentenceFounded { get; private set; } = new();
        public UnityEvent<List<string>> OnWordsInBanList { get; private set; } = new();

        public void SetTargetSentences(List<SentenceConfigs> sentences, int countToPass)
        {
            _targetSentances = new(sentences);
            _countToPass = countToPass;
        }

        public void CalculateSentenceMatching(List<string> words)
        {
            string player = MakeSentence(words).ToLower().Replace(" ", "").Trim();

            if (IsInBanList(words))
            {
                if (!player.Equals(_lastMatchingSentence))
                {
                    OnWordsInBanList.Invoke(words.Intersect(_banWords).ToList());
                }
                _lastMatchingSentence = player;
                return;
            }

            _lastMatchingSentence = player;

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