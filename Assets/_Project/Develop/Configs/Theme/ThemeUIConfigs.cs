using System.Collections;
using System.Collections.Generic;
using Theme;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ThemeUIConfigs", menuName = "Game Configs/Theme/New Theme UI Configs")]
    public class ThemeUIConfigs : ScriptableObject
    {
        [field: Header("Buttons")]
        [field: SerializeField] public ButtonTheme MainButton { get; private set; }
        [field: SerializeField] public ButtonTheme OnButton { get; private set; }
        [field: SerializeField] public ButtonTheme OffButton { get; private set; }
        [field: SerializeField] public ButtonTheme CompletedLevelButton { get; private set; }
        [field: SerializeField] public ButtonTheme UncompletedLevelButton { get; private set; }
        [field: SerializeField] public ButtonTheme CurrentLevelButtonButton { get; private set; }
        
        [field: Space]

        [field: Header("Texts")]
        [field: SerializeField] public TextTheme MainText { get; private set; }
        [field: SerializeField] public TextTheme TemplateSlotText { get; private set; }

        [field: Space]

        [field: Header("Colors")]
        [field: SerializeField] public ColorTheme MainFillColor { get; private set; }
        [field: SerializeField] public ColorTheme MainIconColor { get; private set; }
        [field: SerializeField] public ColorTheme BackgroundColor { get; private set; }
        [field: SerializeField] public ColorTheme SlotColor { get; private set; }
        [field: SerializeField] public ColorTheme CubeRemoveAreaColor { get; private set; }
        [field: SerializeField] public ColorTheme CubeWordListColor { get; private set; }
        [field: SerializeField] public ColorTheme CubeWordSelection { get; private set; }
        [field: SerializeField] public ColorTheme CompletedLevelButtonIcon { get; private set; }
        [field: SerializeField] public ColorTheme UncompletedLevelButtonIcon { get; private set; }
        [field: SerializeField] public ColorTheme CurrentLevelButtonIcon { get; private set; }
        [field: SerializeField] public ColorTheme HighlightInTheoryProgress { get; private set; }

        [field:Space]

        [field: Header("Gradients")]
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
                OnButton,
                OffButton,
                CompletedLevelButton,
                UncompletedLevelButton,
                CurrentLevelButtonButton,
            };
            return _buttons;
        }

        public IEnumerable<TextTheme> GetTexts()
        {
            if (_texts != null) return _texts;

            _texts = new List<TextTheme>()
            {
                MainText,
                TemplateSlotText,
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
                CubeWordListColor,
                CubeWordSelection,
                CompletedLevelButtonIcon,
                UncompletedLevelButtonIcon,
                CurrentLevelButtonIcon,
                HighlightInTheoryProgress,
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
