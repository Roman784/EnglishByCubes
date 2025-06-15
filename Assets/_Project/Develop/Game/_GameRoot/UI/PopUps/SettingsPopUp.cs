using Theme;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SettingsPopUp : PopUp
    {
        [Space]

        [SerializeField] private GameObject _changeMusicVolumeOnButton;
        [SerializeField] private GameObject _changeMusicVolumeOffButton;

        [Space]

        [SerializeField] private GameObject _changeSoundVolumeOnButton;
        [SerializeField] private GameObject _changeSoundVolumeOffButton;

        private new void Awake()
        {
            base.Awake();

            SetActiveMusicVolumeView(true);
            SetActiveSoundVolumeView(true);
        }

        public void ChangeMusicVolume()
        {
            SetActiveMusicVolumeView(!_changeMusicVolumeOnButton.activeSelf);
        }

        public void ChangeSoundVolume()
        {
            SetActiveSoundVolumeView(!_changeSoundVolumeOnButton.activeSelf);
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

        private void SetActiveMusicVolumeView(bool isOn)
        {
            _changeMusicVolumeOnButton.SetActive(isOn);
            _changeMusicVolumeOffButton.SetActive(!isOn);
        }

        private void SetActiveSoundVolumeView(bool isOn)
        {
            _changeSoundVolumeOnButton.SetActive(isOn);
            _changeSoundVolumeOffButton.SetActive(!isOn);
        }

        public class Factory : PopUpFactory<SettingsPopUp>
        {
        }
    }
}