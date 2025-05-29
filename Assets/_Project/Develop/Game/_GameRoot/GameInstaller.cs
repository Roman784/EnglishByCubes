using Configs;
using GameState;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRootPrefab;

        public override void InstallBindings()
        {
            BindSceneLoader();
            BindConfigsProvier();
            BindGameStateProvider();
            BindUI();
        }

        private void BindSceneLoader()
        {
            Container.Bind<SceneLoader>().AsTransient();
        }

        private void BindConfigsProvier()
        {
            Container.Bind<IConfigsProvider>().To<ConfigsProvider>().AsSingle();
        }

        private void BindGameStateProvider()
        {
            Container.Bind<IGameStateProvider>().To<JsonGameStateProvider>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<UIRoot>().FromComponentInNewPrefab(_uiRootPrefab).AsSingle().NonLazy();
        }
    }
}