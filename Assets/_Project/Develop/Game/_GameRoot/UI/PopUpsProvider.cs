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

        [Inject]
        private void Construct(SettingsPopUp.Factory settingsPopUpFactory,
                               LevelCompletionPopUp.Factory levelCompletionPopUp,
                               LevelInfoPopUp.Factory levelInfoPopUp)
        {
            _settingsPopUpFactory = settingsPopUpFactory;
            _levelCompletionPopUp = levelCompletionPopUp;
            _levelInfoPopUp = levelInfoPopUp;
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
    }
}