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

        public void EnableLeftButton()
        {
            _switchToLeftButton.gameObject.SetActive(true);
        }

        public void EnableRightButton()
        {
            _switchToRightButton.gameObject.SetActive(true);
        }

        public void DisableLeftButton()
        {
            _switchToLeftButton.gameObject.SetActive(false);
        }

        public void DisableRightButton()

        {
            _switchToRightButton.gameObject.SetActive(false);
        }
    }
}