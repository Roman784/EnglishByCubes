using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MistakeCorrectionConfigs", menuName = "Game Configs/Level/New Mistake Correction Configs")]
    public class MistakeCorrectionConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.MistakeCorrection;

        [field: Space]

        [field: SerializeField] public List<MistakeCorrectionSentence> Sentences { get; private set; }

        public override void Validate()
        {
            foreach (var sentence in Sentences)
            {
                var targetCubeNumbers = sentence.Slots.Select(s => s.CubeNumber);

                foreach (var cubeNumber in sentence.Sentence.CubesPool)
                {
                    if (!targetCubeNumbers.Contains(cubeNumber))
                        Debug.LogError($"Cube number {cubeNumber} in the sentence \"{sentence.Sentence.SentenceEn}\" not found! {name}");
                }

                foreach (var slot in sentence.Slots)
                {
                    if (!sentence.Sentence.CubesPool.Contains(slot.CubeNumber))
                        Debug.LogError($"Cube number {slot.CubeNumber} in the sentence \"{sentence.Sentence.SentenceEn}\" not found! {name}");
                }
            }
        }

        private void OnValidate()
        {
            Validate();
        }
    }

    [Serializable]
    public class MistakeCorrectionSentence
    {
        [field: SerializeField] public SentenceConfigs Sentence { get; private set; }
        [field: SerializeField] public List<MistakeCorrectionSlotData> Slots { get; private set; }
    }

    [Serializable]
    public class MistakeCorrectionSlotData
    {
        [field: SerializeField] public int CubeNumber { get; private set; }
        [field: SerializeField] public int SideIndex { get; private set; }
    }
}