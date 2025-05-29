using Configs;
using GameState;
using System.Collections;
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

        [Inject]
        private void Construct(UIRoot uiRoot, SceneLoader sceneLoader,
                               IConfigsProvider configsProvider, IGameStateProvider gameStateProvider)
        {
            _uiRoot = uiRoot;
            _sceneLoader = sceneLoader;
            _configsProvider = configsProvider;
            _gameStateProvider = gameStateProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}