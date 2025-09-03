using Gameplay;
using System.Collections;
using UnityEngine;
using Utils;
using R3;
using LevelMenu;
using Theory;

namespace GameRoot
{
    public class GameEntryPoint : SceneEntryPoint
    {
        private void Start()
        {
            var enterParams = new SceneEnterParams(Scenes.BOOT);
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
                            _audioProvider.Init();
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

            _audioProvider.PlayMusic();

            LoadScene();
        }

        private void LoadScene()
        {
#if UNITY_EDITOR
            var sceneName = GameAutostarter.StartScene;
            var defaultBootParams = new SceneEnterParams(Scenes.BOOT);

            if (sceneName == Scenes.GAMEPLAY)
            {
                _sceneProvider.OpenPractice(defaultBootParams, 4);
                return;
            }
            else if (sceneName == Scenes.LEVEL_MENU)
            {
                _sceneProvider.OpenLevelMenu(defaultBootParams);
                return;
            }
            else if (sceneName == Scenes.THEORY)
            {
                _sceneProvider.OpenTheory(defaultBootParams, 1);
                return;
            }
            else if (sceneName == Scenes.COLLECTION)
            {
                _sceneProvider.OpenCollection(defaultBootParams);
                return;
            }
            else if (sceneName == Scenes.TEMPLATE)
            {
                _sceneProvider.OpenTemplate(defaultBootParams, 2);
                return;
            }
            else if (sceneName == Scenes.MISTAKE_CORRECTION)
            {
                _sceneProvider.OpenMistakeCorrection(defaultBootParams, 3);
                return;
            }
#endif
            var enterParams = new LevelMenuEnterParams(); // <- From boot
            _sceneProvider.OpenLevelMenu(enterParams);
        }
    }
}