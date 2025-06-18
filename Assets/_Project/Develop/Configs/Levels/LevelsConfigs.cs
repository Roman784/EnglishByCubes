using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfigs", menuName = "Game Configs/Level/New Levels Configs")]
    public class LevelsConfigs : ScriptableObject
    {
        [field: SerializeField] public List<LevelConfigs> Levels { get; private set; }

        public LevelConfigs GetLevel(int number)
        {
            foreach (var level in Levels)
            {
                if (level.Number == number)
                    return level;
            }

            Debug.LogError($"Level number {number} was not found!");
            return null;
        }

        [ContextMenu("Set Level Numbers")]
        private void SetLevelNumbers()
        {
            for (int i = 0; i < Levels.Count; i++)
            {
                Levels[i].SetNumber(i + 1);
            }

            EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private void OnValidate()
        {
            ValidateLevelNumbers();
        }

        private void ValidateLevelNumbers()
        {
            for (int i = 0; i < Levels.Count; i++)
            {
                var number = Levels[i].Number;
                for (int j = i + 1; j < Levels.Count; j++)
                {
                    if (number == Levels[j].Number)
                        Debug.LogError($"Level numbers {number} are repeated!");
                }
            }
        }
    }
}