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
        protected SceneProvider _sceneProvider;
        protected IConfigsProvider _configsProvider;
        protected IGameStateProvider _gameStateProvider;
        protected ThemeProvider _themeProvider;
        protected PopUpsProvider _rootPopUpsProvider;

        [Inject]
        private void Construct(UIRoot uiRoot, SceneProvider sceneProvider,
                               IConfigsProvider configsProvider, IGameStateProvider gameStateProvider,
                               ThemeProvider themeProvider, PopUpsProvider rootPopUpsProvider)
        {
            _uiRoot = uiRoot;
            _sceneProvider = sceneProvider;
            _configsProvider = configsProvider;
            _gameStateProvider = gameStateProvider;
            _themeProvider = themeProvider;
            _rootPopUpsProvider = rootPopUpsProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;

        protected void CustomizeTheme()
        {
            _themeProvider.CustomizeAll();
        }
    }
}