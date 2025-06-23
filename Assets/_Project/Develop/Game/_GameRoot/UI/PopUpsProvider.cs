using Zenject;

namespace UI
{
    public class PopUpsProvider
    {
        private SettingsPopUp.Factory _settingsPopUpFactory;
        private LevelCompletionPopUp.Factory _levelCompletionPopUp;

        [Inject]
        private void Construct(SettingsPopUp.Factory settingsPopUpFactory, LevelCompletionPopUp.Factory levelCompletionPopUp)
        {
            _settingsPopUpFactory = settingsPopUpFactory;
            _levelCompletionPopUp = levelCompletionPopUp;
        }

        public void OpenSettingsPopUp()
        {
            _settingsPopUpFactory.Create().Open();
        }

        public void OpenLevelCompletionPopUp()
        {
            _levelCompletionPopUp.Create().Open();
        }
    }
}