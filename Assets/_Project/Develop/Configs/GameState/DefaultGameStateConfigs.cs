using Theme;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "DefaultGameStateConfigs", menuName = "Game Configs/New Default Game State Configs")]
    public class DefaultGameStateConfigs : ScriptableObject
    {
        [field: SerializeField] public int LastCompletedLevelNumber { get; private set; }
        [field: SerializeField] public ThemeModes CurrentThemeMode { get; private set; }
    }
}