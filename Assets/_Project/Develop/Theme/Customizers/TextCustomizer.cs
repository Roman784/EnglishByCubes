using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Theme
{
    public class TextCustomizer : ThemeCustomizer
    {
        [SerializeField] private ThemeColorModes _colorMode;

        [Space]

        [SerializeField] private TMP_Text _view;

        protected override void Customize(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetTexts());
            if (theme == null) return;
            
            if (_colorMode == ThemeColorModes.Dark) 
                _view.color = theme.Dark;
            else
                _view.color = theme.Light;
        }

        protected override void ChangeTheme(ThemeConfigs configs)
        {
            var theme = GetTheme(configs.UIConfigs.GetTexts());
            if (theme == null) return;

            Color color;
            if (_colorMode == ThemeColorModes.Dark)
                color = theme.Dark;
            else
                color = theme.Light;

            _view.DOColor(color, 0.25f);
        }
    }
}