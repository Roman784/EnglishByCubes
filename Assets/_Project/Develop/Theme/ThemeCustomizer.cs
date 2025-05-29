using Configs;
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
    }
}