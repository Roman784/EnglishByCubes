using Configs;
using System.Collections.Generic;
using Theme;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SceneUI : MonoBehaviour
    {
        [SerializeField] private List<ThemeCustomizer> _themeCustomizers;

        protected IConfigsProvider _configsProvider;

        [Inject]
        private void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void CustomizeTheme()
        {
            var themeConfigs = _configsProvider.GameConfigs.ThemeConfigs;

            foreach (var customizer in _themeCustomizers)
            {
                customizer.Customize(themeConfigs);
            }
        }
    }
}