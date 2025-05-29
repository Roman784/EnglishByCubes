using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ThemeUIConfigs", menuName = "Game Configs/Theme/New Theme UI Configs")]
    public class ThemeUIConfigs : ScriptableObject
    {
        [field: SerializeField] public List<ButtonTheme> Buttons { get; private set; }
        [field: SerializeField] public List<TextTheme> Texts { get; private set; }

        public class Theme
        {
            [field: SerializeField] public ThemeTags Tag { get; private set; }
        }

        [Serializable]
        public class ButtonTheme : Theme
        {
            [field: SerializeField] public Color Icon { get; private set; }
            [field: SerializeField] public Color Top { get; private set; }
            [field: SerializeField] public Color Side { get; private set; }
        }

        [Serializable]
        public class TextTheme : Theme
        {
            [field: SerializeField] public Color Dark { get; private set; }
            [field: SerializeField] public Color Light { get; private set; }
        }
    }
}
