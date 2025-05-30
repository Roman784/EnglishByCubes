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

        [Inject]
        private void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void CustomizeTheme()
        {
            var customizers = FindObjectsOfType<ThemeCustomizer>();
            var configs = _configsProvider.GameConfigs.ThemeConfigs;

            foreach (var customizer in customizers)
            {
                customizer.Customize(configs);
            }
        }
    }
}