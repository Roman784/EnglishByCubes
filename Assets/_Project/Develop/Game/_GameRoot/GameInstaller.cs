using Configs;
using GameState;
using Theme;
using UI;
using UnityEngine;
using Zenject;

namespace GameRoot
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRootPrefab;

        [Space]

        [SerializeField] private SettingsPopUp _settingsPopUpPrefab;
        [SerializeField] private LevelCompletionPopUp _levelCompletionPopUpPrefab;

        public override void InstallBindings()
        {
            BindProviers();
            BindUI();
        }

        private void BindProviers()
        {
            Container.Bind<SceneProvider>().AsSingle();
            Container.Bind<IConfigsProvider>().To<ConfigsProvider>().AsSingle();
            Container.Bind<IGameStateProvider>().To<JsonGameStateProvider>().AsSingle();
            Container.Bind<ThemeProvider>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<UIRoot>().FromComponentInNewPrefab(_uiRootPrefab).AsSingle().NonLazy();

            BindPopUps();
        }

        private void BindPopUps()
        {
            Container.Bind<PopUpsProvider>().AsTransient();

            Container.BindFactory<SettingsPopUp, SettingsPopUp.Factory>().FromComponentInNewPrefab(_settingsPopUpPrefab);
            Container.BindFactory<LevelCompletionPopUp, LevelCompletionPopUp.Factory>().FromComponentInNewPrefab(_levelCompletionPopUpPrefab);
        }
    }
}