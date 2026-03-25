using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PopUpsProvider
    {
        private SettingsPopUp.Factory _settingsPopUpFactory;
        private LevelCompletionPopUp.Factory _levelCompletionPopUp;
        private LevelInfoPopUp.Factory _levelInfoPopUp;
        private AppInfoPopUp.Factory _appInfoPopUp;
        private FirstEntrancePopUp.Factory _firstEntrancePopUp;
        private GameCompletedPopUp.Factory _gameCompletedPopUp;

        [Inject]
        private void Construct(SettingsPopUp.Factory settingsPopUpFactory,
                               LevelCompletionPopUp.Factory levelCompletionPopUp,
                               LevelInfoPopUp.Factory levelInfoPopUp,
                               AppInfoPopUp.Factory appInfoPopUp,
                               FirstEntrancePopUp.Factory firstEntrancePopUp,
                               GameCompletedPopUp.Factory gameCompletedPopUp)
        {
            _settingsPopUpFactory = settingsPopUpFactory;
            _levelCompletionPopUp = levelCompletionPopUp;
            _levelInfoPopUp = levelInfoPopUp;
            _appInfoPopUp = appInfoPopUp;
            _firstEntrancePopUp = firstEntrancePopUp;
            _gameCompletedPopUp = gameCompletedPopUp;
        }

        public void OpenSettingsPopUp()
        {
            _settingsPopUpFactory.Create().Open();
        }

        public void OpenLevelCompletionPopUp()
        {
            _levelCompletionPopUp.Create().Open();
        }

        public void OpenLevelInfo(GameObject contentPrefab)
        {
            _levelInfoPopUp.Create(contentPrefab).Open();
        }

        public void OpenAppInfo()
        {
            _appInfoPopUp.Create().Open();
        }

        public void OpenFirstEntrance()
        {
            _firstEntrancePopUp.Create().Open();
        }

        public void OpenGameCompletedPopUp()
        {
            _gameCompletedPopUp.Create().Open();
        }
    }
}