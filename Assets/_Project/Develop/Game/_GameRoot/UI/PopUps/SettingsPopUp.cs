using Theme;
using Zenject;

namespace UI
{
    public class SettingsPopUp : PopUp
    {
        private ThemeProvider _themeProvider;

        [Inject]
        private void Construct(ThemeProvider themeProvider)
        {
            _themeProvider = themeProvider;
        }

        public void ChangeMusicVolume()
        {

        }

        public void ChangeSoundVolume()
        {

        }

        public void ChangeMusic()
        {

        }

        public void OpenAppInfo()
        {

        }

        public void ChangeTheme()
        {
            _themeProvider.Switch();
        }

        public void OpenMainMenu()
        {

        }

        public class Factory : PopUpFactory<SettingsPopUp>
        {
        }
    }
}