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
        private ThemeProvider _provider;

        public ThemeTags Tag => _tag;

        public void SetTag(ThemeTags newTag)
        {
            _tag = newTag;

            if (_isCustomized)
                ChangeTheme(_provider.CurrentTheme);
        }

        public void Customize(ThemeProvider themeProvider)
        {
            if (_isCustomized) return;
            
            _isCustomized = true;
            _provider = themeProvider;

            Customize(_provider.CurrentTheme);
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