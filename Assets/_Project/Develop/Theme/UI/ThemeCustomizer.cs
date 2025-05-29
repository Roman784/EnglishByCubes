using Configs;
using UnityEngine;

namespace Theme
{
    public abstract class ThemeCustomizer : MonoBehaviour
    {
        public abstract void Customize(ThemeConfigs configs);

        protected ButtonTheme GetButtonTheme(ThemeConfigs configs, ThemeTags tag)
        {
            foreach (var theme in configs.UIConfigs.Buttons)
            {
                if (theme.Tag == tag) return theme;
            }

            Debug.LogError($"A tag of theme {tag} was not found!");
            return null;
        }
    }
}