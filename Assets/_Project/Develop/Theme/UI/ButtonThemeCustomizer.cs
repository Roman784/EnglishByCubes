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
            base.Customize(configs);

            var theme = GetTheme(configs);
            if (theme == null) return;

            if (_iconView != null) _iconView.color = theme.Icon;
            if (_topView != null) _topView.color = theme.Top;
            if (_sideView != null) _sideView.effectColor = theme.Side;
        }

        private ButtonTheme GetTheme(ThemeConfigs configs)
        {
            foreach (var theme in configs.UIConfigs.Buttons)
            {
                if (theme.Tag == _tag) return theme;
            }

            Debug.LogError($"Tag {tag} of the button theme was not found!");
            return null;
        }
    }
}