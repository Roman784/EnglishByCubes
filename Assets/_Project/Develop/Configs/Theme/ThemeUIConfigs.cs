using System.Collections;
using System.Collections.Generic;
using Theme;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ThemeUIConfigs", menuName = "Game Configs/Theme/New Theme UI Configs")]
    public class ThemeUIConfigs : ScriptableObject
    {
        [field: SerializeField] public ButtonTheme MainButton { get; private set; }
        
        [field: Space]

        [field: SerializeField] public TextTheme MainText { get; private set; }

        [field: Space]

        [field: SerializeField] public ColorTheme MainFillColor { get; private set; }
        [field: SerializeField] public ColorTheme MainIconColor { get; private set; }
        [field: SerializeField] public ColorTheme BackgroundColor { get; private set; }
        [field: SerializeField] public ColorTheme SlotColor { get; private set; }
        [field: SerializeField] public ColorTheme CubeRemoveAreaColor { get; private set; }

        [field:Space]

        [field: SerializeField] public GradientTheme GameplayProgressBarGradient { get; private set; }

        private List<ButtonTheme> _buttons;
        private List<TextTheme> _texts;
        private List<ColorTheme> _colors;
        private List<GradientTheme> _gradients;

        public IEnumerable<ButtonTheme> GetButtons()
        {
            if (_buttons != null) return _buttons;

            _buttons = new List<ButtonTheme>()
            {
                MainButton,
            };
            return _buttons;
        }

        public IEnumerable<TextTheme> GetTexts()
        {
            if (_texts != null) return _texts;

            _texts = new List<TextTheme>()
            {
                MainText,
            };
            return _texts;
        }

        public IEnumerable<ColorTheme> GetColors()
        {
            if (_colors != null) return _colors;

            _colors = new List<ColorTheme>()
            {
                MainFillColor,
                MainIconColor,
                BackgroundColor,
                SlotColor,
                CubeRemoveAreaColor,
            };
            return _colors;
        }

        public IEnumerable<GradientTheme> GetGradients()
        {
            if (_gradients != null) return _gradients;

            _gradients = new List<GradientTheme>()
            {
                GameplayProgressBarGradient,
            };
            return _gradients;
        }
    }
}
