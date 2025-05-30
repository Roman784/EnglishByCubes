using Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Theme
{
    public abstract class ThemeCustomizer : MonoBehaviour
    {
        [SerializeField] private ThemeCustomizer _additionalCustomizer;

        [Space]

        [SerializeField] protected ThemeTags _tag;

        public virtual void Customize(ThemeConfigs configs)
        {
            _additionalCustomizer?.Customize(configs);
        }

        public T GetTheme<T>(IEnumerable<T> themes) where T : ThemeBase
        {
            foreach (var theme in themes)
            {
                if (theme.Tag == _tag)
                    return theme;
            }

            Debug.LogError($"Tag {tag} for theme was not found!");
            return null;
        }
    }
}