using Audio;
using Configs;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Gameplay
{
    public class SlotBarView : MonoBehaviour
    {
        [SerializeField] private List<Slot> _slots;

        [Space]

        [SerializeField] private Button _switchToLeftButton;
        [SerializeField] private Button _switchToRightButton;

        [HideInInspector] public UnityEvent<int> OnSwitched = new();

        public void Init(AudioProvider audioProvider, AudioClip clickSound)
        {
            var cameraAngles = Camera.main.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(cameraAngles.x - 90, 0f, 0f);

            _switchToLeftButton.onClick.AddListener(() => OnSwitched.Invoke(-1));
            _switchToRightButton.onClick.AddListener(() => OnSwitched.Invoke(1));
            
            _switchToLeftButton.GetComponent<ButtonAudioPlayer>().Init(audioProvider, clickSound);
            _switchToRightButton.GetComponent<ButtonAudioPlayer>().Init(audioProvider, clickSound);
        }

        public List<Slot> GetSlots()
        {
            return _slots;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void EnableLeftButton() => EnableButton(_switchToLeftButton);
        public void EnableRightButton() => EnableButton(_switchToRightButton);
        public void DisableLeftButton() => DisableButton(_switchToLeftButton);
        public void DisableRightButton() => DisableButton(_switchToRightButton);

        private void EnableButton(Button button)
        {
            button.enabled = true;
            button.gameObject.SetActive(true);
            button.transform.DOScale(Vector3.one, 0.15f)
                .SetEase(Ease.InOutBounce);
        }

        private void DisableButton(Button button)
        {
            button.enabled = false;
            button.transform.DOScale(Vector3.zero, 0.15f)
                .SetEase(Ease.InOutBounce)
                .OnComplete(() => button.gameObject.SetActive(false));
        }
    }
}