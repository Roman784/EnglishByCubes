using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Theme
{
    public class ColorCustomizer : ThemeCustomizer
    {
        [SerializeField] private Image _view;

        public override void Customize(ThemeConfigs configs)
        {
            base.Customize(configs);

            var theme = GetTheme(configs.UIConfigs.Colors);
            if (theme == null) return;

            _view.color = theme.Color;
        }
    }
}