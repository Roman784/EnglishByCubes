using Configs;
using System.Collections;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected SceneLoader _sceneLoader;
        protected IConfigsProvider _configsProvider;

        [Inject]
        private void Construct(SceneLoader sceneLoader, IConfigsProvider configsProvider)
        {
            _sceneLoader = sceneLoader;
            _configsProvider = configsProvider;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}