using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gameplay
{
    public class SlotBarView : MonoBehaviour
    {
        [SerializeField] private List<Slot> _slots;

        [Space]

        [SerializeField] private Button _switchToLeftButton;
        [SerializeField] private Button _switchToRightButton;

        [HideInInspector] public UnityEvent<int> OnSwitched = new();

        private void Awake()
        {
            _switchToLeftButton.onClick.AddListener(() => OnSwitched.Invoke(-1));
            _switchToRightButton.onClick.AddListener(() => OnSwitched.Invoke(1));
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