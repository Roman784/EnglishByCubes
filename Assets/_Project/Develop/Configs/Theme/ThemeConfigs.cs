using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ThemeConfigs", menuName = "Game Configs/Theme/New Theme Configs")]
    public class ThemeConfigs : ScriptableObject
    {
        [field: SerializeField] public ThemeUIConfigs UIConfigs { get; private set; }
    }
}