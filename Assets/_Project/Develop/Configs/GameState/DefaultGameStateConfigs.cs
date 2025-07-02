using System.Collections.Generic;
using Theme;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "DefaultGameStateConfigs", menuName = "Game Configs/New Default Game State Configs")]
    public class DefaultGameStateConfigs : ScriptableObject
    {
        [field: SerializeField] public int LastCompletedLevelNumber { get; private set; }
        [field: SerializeField] public ThemeModes CurrentThemeMode { get; private set; }
        [field: SerializeField] public List<int> CollectedCollectionItems { get; private set; }
        [field: SerializeField] public float CurrentCollectionItemFill { get; private set; }
        [field: SerializeField] public float MusicVolume { get; private set; }
        [field: SerializeField] public float SoundVolume { get; private set; }
    }
}