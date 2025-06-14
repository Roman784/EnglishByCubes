using Theme;
using Zenject;

namespace UI
{
    public class SettingsPopUp : PopUp
    {
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