using System;
using UnityEngine;

namespace Template
{
    [Serializable]
    public class TemplateSentence
    {
        [field: SerializeField] public string SentenceRu { get; private set; }
        [field: SerializeField] public string SentenceEn { get; private set; }
        [field: SerializeField] public string TargetSentence { get; private set; }
    }
}