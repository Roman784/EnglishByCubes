using System.Collections;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        protected SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public abstract IEnumerator Run<T>(T enterParams) where T : SceneEnterParams;
    }
}