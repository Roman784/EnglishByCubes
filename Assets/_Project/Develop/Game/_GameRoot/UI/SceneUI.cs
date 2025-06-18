using Configs;
using GameRoot;
using GameState;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SceneUI : MonoBehaviour
    {
        protected IConfigsProvider _configsProvider;
        protected IGameStateProvider _gameStateProvider;
        protected SceneLoader _sceneLoader;
        protected PopUpsProvider _popUpsProvider;

        protected GameConfigs GameConfigs => _configsProvider.GameConfigs;
        protected UIConfigs UIConfigs => GameConfigs.UIConfigs;
        protected GameState.GameState GameState => _gameStateProvider.GameStateProxy.State;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, IGameStateProvider gameStateProvider,
                               SceneLoader sceneLoader, PopUpsProvider popUpsProvider)
        {
            _configsProvider = configsProvider;
            _gameStateProvider = gameStateProvider;
            _sceneLoader = sceneLoader;
            _popUpsProvider = popUpsProvider;
        }

        public void OpenSettings()
        {
            _popUpsProvider.OpenSettingsPopUp();
        }
    }
}