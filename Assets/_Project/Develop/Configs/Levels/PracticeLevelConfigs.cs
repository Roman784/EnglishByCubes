using Gameplay;
using System.Collections.Generic;
using UI;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PracticeConfigs", menuName = "Game Configs/Level/New Practice Configs")]
    public class PracticeLevelConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.Practice;

        [field: SerializeField] public List<SentenceConfigs> Sentences { get; private set; }
        [field: SerializeField] public List<int> CubeNumbersPool { get; private set; }

        public override void Validate()
        {
            CubeNumbersPool.Clear();
            foreach (var sentence in Sentences)
            {
                foreach (var cubeNumber in sentence.CubesPool)
                {
                    if (CubeNumbersPool.Contains(cubeNumber)) continue;
                    CubeNumbersPool.Add(cubeNumber);
                }
            }

            CubeNumbersPool.Sort();

            EditorUtility.SetDirty(this);
        }

        private void OnValidate()
        {
            Validate();
        }
    }
}