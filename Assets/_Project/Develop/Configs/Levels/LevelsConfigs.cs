using System.Collections.Generic;
using UI;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfigs", menuName = "Game Configs/Level/New Levels Configs")]
    public class LevelsConfigs : ScriptableObject
    {
        [field: SerializeField] public List<LevelConfigs> Levels { get; private set; }

        public LevelConfigs GetLevel(int number, LevelMode mode)
        {
            foreach (var level in Levels)
            {
                if (level.LocalNumber == number && level.Mode == mode)
                    return level;
            }

            Debug.LogError($"Level number {number} was not found!");
            return null;
        }

        public LevelConfigs GetLevel(int number)
        {
            foreach (var level in Levels)
            {
                if (level.GlobalNumber == number)
                    return level;
            }

            Debug.LogError($"Level number {number} was not found!");
            return null;
        }

        public int GetTheoryNumberForCurrentLevel(int number)
        {
            var theoryNumber = -1;
            foreach (var level in Levels)
            {
                if (level.Mode == LevelMode.Theory)
                    theoryNumber = level.GlobalNumber;

                if (level.GlobalNumber == number)
                    break;
            }

            return theoryNumber;
        }

        [ContextMenu("Set Level Numbers")]
        private void SetLevelNumbers()
        {
            var globalI = 0;
            foreach (var item in GetLevelsMap())
            {
                var levels = item.Value;
                for (int i = 0; i < levels.Count; i++)
                {
                    levels[i].SetLocalNumber(i + 1);
                    Levels[globalI].SetGlobalNumber(globalI + 1);

                    globalI++;
                }
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private Dictionary<LevelMode, List<LevelConfigs>> GetLevelsMap()
        {
            var map = new Dictionary<LevelMode, List<LevelConfigs>>();
            foreach (var level in Levels)
            {
                if (!map.ContainsKey(level.Mode))
                    map[level.Mode] = new();

                map[level.Mode].Add(level);
            }

            return map;
        }

        private void OnValidate()
        {
            ValidateLevelNumbers();
        }

        private void ValidateLevelNumbers()
        {
            foreach (var item in GetLevelsMap())
            {
                var levels = item.Value;
                for (int i = 0; i < levels.Count; i++)
                {
                    var number = levels[i].LocalNumber;
                    for (int j = i + 1; j < levels.Count; j++)
                    {
                        if (number == levels[j].LocalNumber)
                            Debug.LogError($"Level numbers {number} are repeated at the {item.Key} mode!");
                    }
                }
            }

            for (int i = 0; i < Levels.Count; i++)
            {
                var number = Levels[i].GlobalNumber;
                for (int j = i + 1; j < Levels.Count; j++)
                {
                    if (number == Levels[j].GlobalNumber)
                        Debug.LogError($"Level numbers {number} are repeated!");
                }
            }
        }
    }
}