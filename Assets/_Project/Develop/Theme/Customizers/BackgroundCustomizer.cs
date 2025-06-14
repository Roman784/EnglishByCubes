using Configs;
using DG.Tweening;
using UnityEngine;

namespace Theme
{
    public class BackgroundCustomizer : ThemeCustomizer
    {
        [Space]

        [SerializeField] private Camera _view;

        protected override void Customize(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetColors());
            if (theme == null) return;

            _view.backgroundColor = theme.Color;
        }

        protected override void ChangeTheme(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetColors());
            if (theme == null) return;

            _view.DOColor(theme.Color, 0.25f);
        }
    }
}