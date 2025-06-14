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
        protected ThemeProvider _themeProvider;
        protected PopUpsProvider _popUpsProvider;

        protected GameConfigs GameConfigs => _configsProvider.GameConfigs;
        protected UIConfigs UIConfigs => GameConfigs.UIConfigs;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, ThemeProvider themeProvider,
                               PopUpsProvider popUpsProvider)
        {
            _configsProvider = configsProvider;
            _themeProvider = themeProvider;
            _popUpsProvider = popUpsProvider;
        }

        public void OpenSettings()
        {
            _popUpsProvider.OpenSettingsPopUp();
        }

        public virtual void CustomizeTheme()
        {
            var customizers = FindObjectsOfType<ThemeCustomizer>();
            foreach (var customizer in customizers)
            {
                customizer.Customize(_themeProvider);
            }
        }
    }
}