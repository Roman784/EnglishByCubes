using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TaskConfigs", menuName = "Game Configs/Task/New Task Configs")]
    public class TaskConfigs : ScriptableObject
    {
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public string SentenceRu { get; private set; }
        [field: SerializeField] public string SentenceEn { get; private set; }
        [field: SerializeField] public List<string> WordsOrder { get; private set; }
        [field: SerializeField] public List<int> CubeNumbersPool { get; private set; }

        public void SetNumber(int number)
        {
            Number = number;
        }
    }
}