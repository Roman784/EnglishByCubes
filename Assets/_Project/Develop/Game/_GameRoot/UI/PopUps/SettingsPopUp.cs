using Audio;
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

        private float _soundVolume;
        private float _musicVolume;

        private new void Awake()
        {
            base.Awake();

            _view.alpha = 0f;
        }

        public override void Open()
        {
            _pauseProvider.StopGame();

            _musicVolume = _audioProvider.MusicVolume.Value;
            _soundVolume = _audioProvider.SoundVolume.Value;

            SetAudioButtonTag(_changeMusicVolumeButton, _musicVolume);
            SetAudioButtonTag(_changeSoundVolumeButton, _soundVolume);

            base.Open();
        }

        public override void Close()
        {
            _pauseProvider.ContinueGame();

            base.Close();
        }

        public void ChangeMusicVolume()
        {
            _musicVolume = _musicVolume > 0f ? 0f : 1f;
            _audioProvider.MusicVolume.Value = _musicVolume;

            SetAudioButtonTag(_changeMusicVolumeButton, _musicVolume);
        }

        public void ChangeSoundVolume()
        {
            _soundVolume = _soundVolume > 0f ? 0f : 1f;
            _audioProvider.SoundVolume.Value = _soundVolume;

            SetAudioButtonTag(_changeSoundVolumeButton, _soundVolume);
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

        private void SetAudioButtonTag(ButtonCustomizer button, float volume)
        {
            if (volume < 1)
                button.SetTag(ThemeTags.OffButton);
            else
                button.SetTag(ThemeTags.OnButton);
        }

        public class Factory : PopUpFactory<SettingsPopUp>
        {
        }
    }
}