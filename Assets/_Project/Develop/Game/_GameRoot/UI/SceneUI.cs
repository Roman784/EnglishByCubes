using Audio;
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
        protected SceneProvider _sceneProvider;
        protected PopUpsProvider _popUpsProvider;
        protected AudioProvider _audioProvider;

        protected GameConfigs GameConfigs => _configsProvider.GameConfigs;
        protected UIConfigs UIConfigs => GameConfigs.UIConfigs;
        protected UIAudioConfigs AudioConfigs => GameConfigs.AudioConfigs.UIConfigs;
        protected GameState.GameState GameState => _gameStateProvider.GameStateProxy.State;

        [Inject]
        private void Construct(IConfigsProvider configsProvider, IGameStateProvider gameStateProvider,
                               SceneProvider sceneProvider, PopUpsProvider popUpsProvider,
                               AudioProvider audioProvider)
        {
            _configsProvider = configsProvider;
            _gameStateProvider = gameStateProvider;
            _sceneProvider = sceneProvider;
            _popUpsProvider = popUpsProvider;
            _audioProvider = audioProvider;
        }

        public void OpenSettings()
        {
            _popUpsProvider.OpenSettingsPopUp();
        }

        public void OpenLevelCompletionPopUp()
        {
            _popUpsProvider.OpenLevelCompletionPopUp();
        }
    }
}