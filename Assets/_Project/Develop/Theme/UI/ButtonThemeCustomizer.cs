using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Theme
{
    public class ButtonThemeCustomizer : ThemeCustomizer
    {
        [Space]

        [SerializeField] private Image _iconView;
        [SerializeField] private Image _topView;
        [SerializeField] private Shadow _sideView;

        public override void Customize(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.Buttons);
            if (theme == null) return;

            if (_iconView != null) _iconView.color = theme.Icon;
            if (_topView != null) _topView.color = theme.Top;
            if (_sideView != null) _sideView.effectColor = theme.Side;
        }
    }
}