using Configs;
using GameState;
using System.Collections;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected SceneLoader _sceneLoader;
        protected IConfigsProvider _configsProvider;
        protected IGameStateProvider _gameStateProvider;

        [Inject]
        private void Construct(SceneLoader sceneLoader, 
                               IConfigsProvider configsProvider, IGameStateProvider gameStateProvider)
        {
            _sceneLoader = sceneLoader;
            _configsProvider = configsProvider;
            _gameStateProvider = gameStateProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}