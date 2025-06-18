using Configs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Theme
{
    public abstract class ThemeCustomizer : MonoBehaviour
    {
        [SerializeField] protected ThemeTags _tag;

        private bool _isCustomized;
        private ThemeConfigs _currentThemeConfigs;

        public void SetTag(ThemeTags newTag)
        {
            _tag = newTag;

            if (_isCustomized)
                ChangeTheme(_currentThemeConfigs);
        }

        public void Customize(ThemeProvider themeProvider)
        {
            if (_isCustomized) return;
            
            _isCustomized = true;
            _currentThemeConfigs = themeProvider.CurrentTheme;

            Customize(_currentThemeConfigs);
            themeProvider.OnThemeChanged.AddListener(ChangeTheme);
        }

        protected abstract void Customize(ThemeConfigs configs);

        protected virtual void ChangeTheme(ThemeConfigs configs)
        {
            Customize(configs);
        }

        protected T GetTheme<T>(IEnumerable<T> themes) where T : ThemeBase
        {
            foreach (var theme in themes)
            {
                if (theme.Tag == _tag)
                    return theme;
            }

            Debug.LogError($"Tag {_tag} for theme was not found!");
            return null;
        }
    }
}