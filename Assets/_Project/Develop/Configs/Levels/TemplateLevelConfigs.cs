using System.Collections.Generic;
using System.Linq;
using Template;
using UI;
using UnityEditor;
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
                if (sentence.TargetSentence == "")
                    Debug.LogError($"Sentence not found! {name}");

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

#if UNITY_EDITOR
        [ContextMenu("Fill Sentances")]
        private void FillSentances()
        {
            if (Sentences.Count > 0) return;

            var sentanceConfigs = new List<SentenceConfigs>();
            var guids = AssetDatabase.FindAssets($"t:{typeof(SentenceConfigs).Name}",
                new[] { "Assets/_Project/Configs/Sentances" });

            foreach (string guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var config = AssetDatabase.LoadAssetAtPath<SentenceConfigs>(path);

                if (config != null)
                    sentanceConfigs.Add(config);
            }

            foreach (var sentence in sentanceConfigs)
            {
                if (sentence.CubesPool.Length != Slots.Count) continue;

                var f = true;
                for (int i = 0; i < Slots.Count; i++)
                {
                    if (Slots[i].CubeNumber != sentence.CubesPool[i])
                    {
                        f = false;
                        break;
                    }

                }

                if (f)
                    Sentences.Add(sentence);
            }
        }
#endif
    }
}