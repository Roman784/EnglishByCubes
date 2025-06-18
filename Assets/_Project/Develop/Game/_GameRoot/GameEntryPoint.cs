using Gameplay;
using System.Collections;
using UnityEngine;
using Utils;
using R3;
using LevelMenu;

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

            _configsProvider.LoadGameConfigs().Subscribe(res =>
            {
                if (res)
                {
                    _gameStateProvider.LoadGameState().Subscribe(res =>
                    {
                        if (res)
                        {
                            _themeProvider.SetTheme();

                            isLoaded = true;
                        }
                        else
                        {
                            Debug.LogError("Failed to load the game configs!");
                        }
                    });
                }
                else
                {
                    Debug.LogError("Failed to load the game state!");
                }
            });

            yield return new WaitUntil(() => isLoaded);

            LoadScene();
        }

        private void LoadScene()
        {
#if UNITY_EDITOR
            var sceneName = GameAutostarter.StartScene;

            if (sceneName == Scenes.GAMEPLAY)
            {
                var defaultGameplayEnterParams = new GameplayEnterParams(Scenes.BOOT);
                _sceneLoader.LoadAndRunGameplay(defaultGameplayEnterParams);
                return;
            }
            else if (sceneName == Scenes.LEVEL_MENU)
            {
                var defaultLevelMenuEnterParams = new LevelMenuEnterParams(Scenes.BOOT);
                _sceneLoader.LoadAndRunLevelMenu(defaultLevelMenuEnterParams);
                return;
            }
#endif
            var enterParams = new GameplayEnterParams(Scenes.BOOT);
            _sceneLoader.LoadAndRunGameplay(enterParams);

        }
    }
}