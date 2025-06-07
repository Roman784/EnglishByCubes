using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Game Configs/New Game Configs")]
    public class GameConfigs : ScriptableObject
    {
        [field: SerializeField] public CubesConfigs CubesConfigs { get; private set; }
        [field: SerializeField] public ThemeConfigs ThemeConfigs { get; private set; }
    }
}
