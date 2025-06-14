using Configs;
using UnityEngine;

namespace Theme
{
    public class BackgroundCustomizer : ThemeCustomizer
    {
        [Space]

        [SerializeField] private Camera _view;

        protected override void Customize(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.Colors);
            if (theme == null) return;

            _view.backgroundColor = theme.Color;
        }
    }
}