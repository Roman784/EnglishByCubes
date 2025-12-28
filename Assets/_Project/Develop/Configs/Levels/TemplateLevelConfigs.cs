using System.Collections.Generic;
using System.Linq;
using Template;
using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TemplateConfigs", menuName = "Game Configs/Level/New Template Configs")]
    public class TemplateLevelConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.Template;

        [field: Space]

        [field: SerializeField] public int SentencesCount { get; private set; }
        [field: SerializeField] public List<TemplateSlotData> Slots { get; private set; }
        [field: SerializeField] public List<SentenceConfigs> Sentences { get; private set; }

        public override void Validate()
        {
            var targetCubeNumbers = Slots.Select(s => s.CubeNumber);

            foreach (var sentence in Sentences)
            {
                foreach (var cubeNumber in sentence.CubesPool)
                {
                    if (!targetCubeNumbers.Contains(cubeNumber))
                        Debug.LogError($"Cube number {cubeNumber} in the sentence \"{sentence.SentenceEn}\" not found! {name}");
                }

                foreach (var slot in Slots)
                {
                    if (!sentence.CubesPool.Contains(slot.CubeNumber))
                        Debug.LogError($"Cube number {slot.CubeNumber} in the sentence \"{sentence.SentenceEn}\" not found! {name}");
                }
            }
        }

        private void OnValidate()
        {
            Validate();
        }
    }
}