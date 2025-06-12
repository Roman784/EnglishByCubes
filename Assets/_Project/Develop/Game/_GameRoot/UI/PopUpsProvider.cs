using Zenject;

namespace UI
{
    public class PopUpsProvider
    {
        private SettingsPopUp.Factory _settingsPopUpFactory;

        [Inject]
        private void Construct(SettingsPopUp.Factory settingsPopUpFactory)
        {
            _settingsPopUpFactory = settingsPopUpFactory;
        }

        public void OpenSettingsPopUp()
        {
            _settingsPopUpFactory.Create().Open();
        }
    }
}