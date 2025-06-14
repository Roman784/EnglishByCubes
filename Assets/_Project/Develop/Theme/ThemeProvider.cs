using Configs;
using GameState;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using Zenject;

namespace Theme
{
    public class ThemeProvider
    {
        private Dictionary<ThemeModes, ThemeConfigs> _themesMap = new();
        private ThemeModes _currentMode;

        private IGameStateProvider _gameStateProvider;
        private IConfigsProvider _configsProvider;

        public ThemeConfigs CurrentTheme => _themesMap[_currentMode];
        public UnityEvent<ThemeConfigs> OnThemeChanged = new();

        [Inject]
        private void Construct(IGameStateProvider gameStateProvider, IConfigsProvider configsProvider)
        {
            _gameStateProvider = gameStateProvider;
            _configsProvider = configsProvider;
        }

        public void SetTheme()
        {
            _currentMode = (ThemeModes)_gameStateProvider.GameStateProxy.State.CurrentThemeMode;

            _themesMap[ThemeModes.Light] = _configsProvider.GameConfigs.LightThemeConfigs;
            _themesMap[ThemeModes.Dark] = _configsProvider.GameConfigs.DarkThemeConfigs;
        }

        public void Switch()
        {
            var allModes = _themesMap.Keys.ToList();
            var currentIndex = allModes.IndexOf(_currentMode);
            var nextIndex = ++currentIndex % allModes.Count;

            _currentMode = allModes[nextIndex];
            OnThemeChanged.Invoke(CurrentTheme);

            _gameStateProvider.GameStateProxy.SetCurrentThemeMode(_currentMode);
        }
    }
}