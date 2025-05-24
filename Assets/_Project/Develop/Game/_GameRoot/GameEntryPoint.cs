using Gameplay;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Zenject;
using R3;

namespace GameRoot
{
    public class GameEntryPoint : SceneEntryPoint
    {
        private void Start()
        {
            var enterParams = new SceneEnterParams("", "");
            Coroutines.Start(Run(enterParams));
        }

        public override IEnumerator Run<T>(T _)
        {
            var isLoaded = false;

            _configsProvider.LoadGameConfigs().Subscribe(_ =>
            {
                isLoaded = true;
            });

            yield return new WaitUntil(() => isLoaded);

            LoadScene();
        }

        private void LoadScene()
        {
            var sceneName = SceneManager.GetActiveScene().name;

#if UNITY_EDITOR
            if (sceneName == Scenes.GAMEPLAY)
            {
                var defaultGameplayEnterParams = new GameplayEnterParams(Scenes.BOOT);
                _sceneLoader.LoadAndRunGameplay(defaultGameplayEnterParams);
                return;
            }
#endif
            var enterParams = new GameplayEnterParams(Scenes.BOOT);
            _sceneLoader.LoadAndRunGameplay(enterParams);

        }
    }
}