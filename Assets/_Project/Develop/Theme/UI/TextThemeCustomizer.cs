using Configs;
using TMPro;
using UnityEngine;

namespace Theme
{
    public class TextThemeCustomizer : ThemeCustomizer
    {
        [SerializeField] private ThemeColorModes _colorMode;

        [Space]

        [SerializeField] private TMP_Text _textView;

        public override void Customize(ThemeConfigs configs)
        {
            base.Customize(configs);

            var theme = GetTheme(configs.UIConfigs.Texts);

            if (theme == null) return;
            if (_textView == null) return;
            
            if (_colorMode == ThemeColorModes.Dark) 
                _textView.color = theme.Dark;
            else
                _textView.color = theme.Light;
        }
    }
}