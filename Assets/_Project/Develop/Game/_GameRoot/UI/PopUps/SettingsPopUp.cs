using Theme;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsPopUp : PopUp
    {
        [Space]

        [SerializeField] private ButtonCustomizer _changeMusicVolumeButton;
        [SerializeField] private ButtonCustomizer _changeSoundVolumeButton;

        private new void Awake()
        {
            base.Awake();

            _view.alpha = 0f;

            _changeMusicVolumeButton.SetTag(ThemeTags.OnButton);
            _changeSoundVolumeButton.SetTag(ThemeTags.OnButton);
        }

        public override void Open()
        {
            _pauseProvider.StopGame();

            base.Open();
        }

        public override void Close()
        {
            _pauseProvider.ContinueGame();

            base.Close();
        }

        public void ChangeMusicVolume()
        {
            SwitchButtonTag(_changeMusicVolumeButton);
        }

        public void ChangeSoundVolume()
        {
            SwitchButtonTag(_changeSoundVolumeButton);
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

        private void SwitchButtonTag(ButtonCustomizer button)
        {
            if (button.Tag == ThemeTags.OnButton)
                button.SetTag(ThemeTags.OffButton);
            else
                button.SetTag(ThemeTags.OnButton);
        }

        public class Factory : PopUpFactory<SettingsPopUp>
        {
        }
    }
}