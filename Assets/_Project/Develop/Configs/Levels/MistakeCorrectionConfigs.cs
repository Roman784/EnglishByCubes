using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
                if (sentence.Sentence.TargetSentence == "")
                    Debug.LogError($"Sentence not found! {name}");

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

#if UNITY_EDITOR
        [ContextMenu("Fill Slots")]
        private void FillSlots()
        {
            var cubesConfigs = new List<CubeConfigs>();
            var guids = AssetDatabase.FindAssets($"t:{typeof(CubeConfigs).Name}", 
                new[] { "Assets/_Project/Configs/Cubes" });

            foreach (string guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var config = AssetDatabase.LoadAssetAtPath<CubeConfigs>(path);

                if (config != null)
                    cubesConfigs.Add(config);
            }

            foreach (var sentence in Sentences)
            {
                if (sentence.Slots.Count != 0) continue;

                foreach (var cubeNumber in sentence.Sentence.CubesPool)
                {
                    var targetEn = sentence.Sentence.SentenceEn
                            .ToLower()
                            .Replace(".", " ").Replace(",", " ").Replace("?", " ").Replace("!", " ");

                    if (targetEn.Contains("she ")) targetEn = targetEn.Replace("she ", "he/she");
                    else if (targetEn.Contains("he ")) targetEn = targetEn.Replace("he ", "he/she");

                    var words = targetEn.Split(" ");

                    var sideIdx = -1;
                    foreach (var cube in cubesConfigs)
                    {
                        if (cube.Number != cubeNumber) continue;

                        for (int i = 0; i < cube.Words.Count; i++)
                        {
                            if (words.Contains(cube.Words[i]))
                            {
                                sideIdx = i;
                                break;
                            }
                        } 
                    }

                    sentence.Slots.Add(new MistakeCorrectionSlotData()
                    {
                        CubeNumber = cubeNumber,
                        SideIndex = sideIdx,
                    });
                }
            }
        }
#endif

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
        [field: SerializeField] public int CubeNumber { get; set; }
        [field: SerializeField] public int SideIndex { get; set; }
    }
}