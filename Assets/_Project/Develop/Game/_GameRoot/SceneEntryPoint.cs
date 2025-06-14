using Configs;
using GameState;
using System.Collections;
using Theme;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected UIRoot _uiRoot;
        protected SceneLoader _sceneLoader;
        protected IConfigsProvider _configsProvider;
        protected IGameStateProvider _gameStateProvider;
        protected ThemeProvider _themeProvider;

        [Inject]
        private void Construct(UIRoot uiRoot, SceneLoader sceneLoader,
                               IConfigsProvider configsProvider, IGameStateProvider gameStateProvider,
                               ThemeProvider themeProvider)
        {
            _uiRoot = uiRoot;
            _sceneLoader = sceneLoader;
            _configsProvider = configsProvider;
            _gameStateProvider = gameStateProvider;
            _themeProvider = themeProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;

        protected virtual void CustomizeTheme()
        {
            var customizers = FindObjectsOfType<ThemeCustomizer>();
            foreach (var customizer in customizers)
            {
                customizer.Customize(_themeProvider);
            }
        }
    }
}