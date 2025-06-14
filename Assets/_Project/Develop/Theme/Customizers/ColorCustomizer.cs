using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Theme
{
    public class ColorCustomizer : ThemeCustomizer
    {
        [SerializeField] private Image _view;

        protected override void Customize(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetColors());
            if (theme == null) return;

            _view.color = theme.Color;
        }

        protected override void ChangeTheme(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetColors());
            if (theme == null) return;

            _view.DOColor(theme.Color, 0.25f);
        }
    }
}