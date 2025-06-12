using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PopUpsRoot : MonoBehaviour
    {
        [SerializeField] private Transform _popUpsContainer;

        private Stack<PopUp> _popUps = new();

        public void AttachPopUp(PopUp popUp)
        {
            popUp.transform.SetParent(_popUpsContainer, false);
        }

        public void DestroyAllPopUps()
        {
            foreach (var popUp in _popUps)
            {
                popUp?.Destroy();
            }

            _popUps = new();
        }
    }
}