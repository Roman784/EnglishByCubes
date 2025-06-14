using Configs;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        protected override void ChangeTheme(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetButtons());
            if (theme == null) return;

            _topView.DOColor(theme.Top, 0.25f);
            DOTween.To(
                () => _sideView.effectColor,
                x => _sideView.effectColor = x,
                theme.Side,
                0.25f
            );
        }
    }
}