using Configs;
using System.Collections.Generic;
using Theme;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SceneUI : MonoBehaviour
    {
        protected IConfigsProvider _configsProvider;

        protected ThemeConfigs ThemeConfigs => _configsProvider.GameConfigs.ThemeConfigs;

        [Inject]
        private void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void CustomizeTheme()
        {
            var customizers = FindObjectsOfType<ThemeCustomizer>();
            foreach (var customizer in customizers)
            {
                customizer.Customize(ThemeConfigs);
            }
        }
    }
}