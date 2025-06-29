using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PracticeConfigs", menuName = "Game Configs/Level/New Practice Configs")]
    public class PracticeLevelConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.Practice;

        [field: SerializeField] public string SentenceRu { get; private set; }
        [field: SerializeField] public string SentenceEn { get; private set; }
        [field: SerializeField] public List<int> CubeNumbersPool { get; private set; }
    }
}