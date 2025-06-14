using Configs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Theme
{
    public abstract class ThemeCustomizer : MonoBehaviour
    {
        [SerializeField] protected ThemeTags _tag;

        public void Customize(ThemeProvider themeProvider)
        {
            Customize(themeProvider.CurrentTheme);
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