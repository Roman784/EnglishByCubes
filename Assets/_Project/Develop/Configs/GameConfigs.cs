using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Game Configs/New Game Configs")]
    public class GameConfigs : ScriptableObject
    {
        [field: SerializeField] public DefaultGameStateConfigs DefaultGameStateConfigs { get; private set; }

        [field: Space]

        [field: SerializeField] public CubesConfigs CubesConfigs { get; private set; }
        [field: SerializeField] public LevelsConfigs LevelsConfigs { get; private set; }
        [field: SerializeField] public CollectionConfigs CollectionConfigs { get; private set; }
        [field: SerializeField] public UIConfigs UIConfigs { get; private set; }

        [field: Space]

        [field: SerializeField] public ThemeConfigs LightThemeConfigs { get; private set; }
        [field: SerializeField] public ThemeConfigs DarkThemeConfigs { get; private set; }
    }
}
