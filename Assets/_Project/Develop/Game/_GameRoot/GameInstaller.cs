using Audio;
using Configs;
using GameState;
using Pause;
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
        [SerializeField] private LevelInfoPopUp _levelInfoPopUpPrefab;
        [SerializeField] private AppInfoPopUp _appInfoPopUpPrefab;

        [Space]

        [SerializeField] private AudioSourcer _audioSourcerPrefab;

        public override void InstallBindings()
        {
            BindPrefabs();
            BindProviers();
            BindUI();
        }

        private void BindPrefabs()
        {
            Container.Bind<AudioSourcer>().FromInstance(_audioSourcerPrefab).AsTransient();
        }

        private void BindProviers()
        {
            Container.Bind<SceneProvider>().AsSingle();
            Container.Bind<IConfigsProvider>().To<ConfigsProvider>().AsSingle();
            Container.Bind<IGameStateProvider>().To<JsonGameStateProvider>().AsSingle();
            Container.Bind<ThemeProvider>().AsSingle();
            Container.Bind<PauseProvider>().AsSingle();
            Container.Bind<AudioProvider>().AsSingle();
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
            Container.BindFactory<LevelInfoPopUp, LevelInfoPopUp.Factory>().FromComponentInNewPrefab(_levelInfoPopUpPrefab);
            Container.BindFactory<AppInfoPopUp, AppInfoPopUp.Factory>().FromComponentInNewPrefab(_appInfoPopUpPrefab);
        }
    }
}