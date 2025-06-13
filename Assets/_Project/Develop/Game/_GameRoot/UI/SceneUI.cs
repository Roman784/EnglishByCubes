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
        protected PopUpsProvider _popUpsProvider;

        protected GameConfigs GameConfigs => _configsProvider.GameConfigs;
        protected ThemeConfigs ThemeConfigs => GameConfigs.ThemeConfigs;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, PopUpsProvider popUpsProvider)
        {
            _configsProvider = configsProvider;
            _popUpsProvider = popUpsProvider;
        }

        public void OpenSettings()
        {
            _popUpsProvider.OpenSettingsPopUp();
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