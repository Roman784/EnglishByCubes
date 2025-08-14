using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public class TranslationSentenceData
    {
        [field: SerializeField] public string SentenceRu { get; private set; }
        [field: SerializeField] public string SentenceEn { get; private set; }
        [field: SerializeField] public string TargetSentence { get; private set; }
    }
}