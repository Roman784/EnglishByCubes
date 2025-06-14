using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Theme
{
    public class ButtonCustomizer : ThemeCustomizer
    {
        [Space]

        [SerializeField] private Image _topView;
        [SerializeField] private Shadow _sideView;

        protected override void Customize(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetButtons());
            if (theme == null) return;

            _topView.color = theme.Top;
            _sideView.effectColor = theme.Side;
        }
    }
}