using Configs;
using GameState;
using System.Collections.Generic;
using Zenject;

namespace Theme
{
    public class ThemeProvider
    {
        private Dictionary<ThemeModes, ThemeConfigs> _themesMap = new();

        private IGameStateProvider _gameStateProvider;
        private IConfigsProvider _configsProvider;

        public ThemeModes CurrentMode { get; private set; }
        public ThemeConfigs CurrentTheme => _themesMap[CurrentMode]; 

        [Inject]
        private void Construct(IGameStateProvider gameStateProvider, IConfigsProvider configsProvider)
        {
            _gameStateProvider = gameStateProvider;
            _configsProvider = configsProvider;
        }

        public void SetTheme()
        {
            CurrentMode = (ThemeModes)_gameStateProvider.GameStateProxy.State.CurrentThemeMode;

            _themesMap[ThemeModes.Light] = _configsProvider.GameConfigs.LightThemeConfigs;
            _themesMap[ThemeModes.Dark] = _configsProvider.GameConfigs.DarkThemeConfigs;
        }

        public void Switch()
        {

        }
    }
}