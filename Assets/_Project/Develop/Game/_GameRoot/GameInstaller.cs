using Configs;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIRootView _uiRootView;

        public override void InstallBindings()
        {
            BindSceneLoader();
            BindConfigsProvier();
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

        private void BindUI()
        {
            var view = Instantiate(_uiRootView);
            var uiRoot = new UIRoot(_uiRootView);
            DontDestroyOnLoad(view.gameObject);

            Container.Bind<UIRoot>().FromInstance(uiRoot).AsSingle().NonLazy();
        }
    }
}