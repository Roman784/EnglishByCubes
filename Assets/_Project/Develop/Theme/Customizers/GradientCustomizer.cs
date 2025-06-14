using Configs;
using UnityEngine;

namespace Theme
{
    public class GradientCustomizer : ThemeCustomizer
    {
        private ThemeConfigs _configs;

        protected override void Customize(ThemeConfigs configs)
        {
            _configs = configs;
        }

        public Color GetColor(float value)
        {
            if (_configs == null) return Color.red;

            var theme = GetTheme(_configs.UIConfigs.GetGradients());
            if (theme == null) return Color.red;

            return theme.Gradent.Evaluate(value);
        }
    }
}