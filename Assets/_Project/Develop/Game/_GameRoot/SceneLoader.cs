using Gameplay;
using LevelMenu;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Zenject;

namespace GameRoot
{
    public class SceneLoader
    {
        private Coroutine _loading;
        private UIRoot _uiRoot;

        [Inject]
        private void Construct(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public void LoadAndRunGameplay(GameplayEnterParams enterParams)
        {
            StopLoading();
            Coroutines.Start(LoadAndRunScene<GameplayEntryPoint, GameplayEnterParams>
                (Scenes.GAMEPLAY, enterParams));
        }

        public void LoadAndRunLevelMenu(LevelMenuEnterParams enterParams)
        {
            StopLoading();
            Coroutines.Start(LoadAndRunScene<LevelMenuEntryPoint, LevelMenuEnterParams>
                (Scenes.LEVEL_MENU, enterParams));
        }

        private IEnumerator LoadAndRunScene<TEntryPoint, TEnterParams>(string sceneName, TEnterParams enterParams)
            where TEntryPoint : SceneEntryPoint
            where TEnterParams : SceneEnterParams
        {
            yield return _uiRoot.SetLoadingScreen(true);

            _uiRoot.ClearAllContainers();

            yield return LoadScene(sceneName);

            var sceneEntryPoint = Object.FindObjectOfType<TEntryPoint>();
            yield return sceneEntryPoint.Run(enterParams);

            yield return _uiRoot.SetLoadingScreen(false);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
        }

        private void StopLoading()
        {
            if (_loading != null)
                Coroutines.Stop(_loading);
        }
    }
}