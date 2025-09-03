using Configs;
using GameRoot;
using System;
using System.Collections.Generic;
using Template;
using UnityEngine.Events;

namespace MistakeCorrection
{
    public class MistakeCorrectionLevelPassingService : ILevelPassingService
    {
        private List<MistakeCorrectionSentence> _targetSentances = new();

        public UnityEvent<TemplateSentence, int> OnNewSentenceFounded { get; private set; } = new();

        public void SetTargetSentences(List<MistakeCorrectionSentence> sentences)
        {
            _targetSentances = new(sentences);
        }

        public void CalculateSentenceMatching(string playerSentence)
        {
            throw new System.NotImplementedException();
        }
    }
}