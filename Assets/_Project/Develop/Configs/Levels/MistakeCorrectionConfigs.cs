using System;
using System.Collections.Generic;
using Template;
using UI;
using UnityEngine;
using TemplateSentence = Template.TemplateSentence;

namespace Configs
{
    [CreateAssetMenu(fileName = "MistakeCorrectionConfigs", menuName = "Game Configs/Level/New Mistake Correction Configs")]
    public class MistakeCorrectionConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.MistakeCorrection;

        [field: Space]

        [field: SerializeField] public List<MistakeCorrectionSentence> Sentences { get; private set; }
    }

    [Serializable]
    public class MistakeCorrectionSentence
    {
        [field: SerializeField] public TemplateSentence Sentence { get; private set; }
        [field: SerializeField] public List<MistakeCorrectionSlotData> Slots { get; private set; }
    }

    [Serializable]
    public class MistakeCorrectionSlotData
    {
        [field: SerializeField] public int CubeNumber { get; private set; }
        [field: SerializeField] public int SideIndex { get; private set; }
    }
}