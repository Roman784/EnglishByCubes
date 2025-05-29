using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Theme
{
    public class ButtonThemeCustomizer : ThemeCustomizer
    {
        [SerializeField] private ThemeTags _tag;

        [Space]

        [SerializeField] private Image _iconView;
        [SerializeField] private Image _topView;
        [SerializeField] private Shadow _sideView;

        public override void Customize(ThemeConfigs configs)
        {
            var theme = GetButtonTheme(configs, _tag);
            if (theme == null) return;

            _iconView.color = theme.Icon;
            _topView.color = theme.Top;
            _sideView.effectColor = theme.Side;
        }
    }
}