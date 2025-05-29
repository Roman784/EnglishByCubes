using System;
using System.Collections.Generic;
using Theme;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ThemeUIConfigs", menuName = "Game Configs/Theme/New Theme UI Configs")]
    public class ThemeUIConfigs : ScriptableObject
    {
        [field: SerializeField] public List<ButtonTheme> Buttons { get; private set; }
        [field: SerializeField] public List<TextTheme> Texts { get; private set; }
    }
}
