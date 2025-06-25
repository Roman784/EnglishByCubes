using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PopUpsRoot : MonoBehaviour
    {
        [SerializeField] private Transform _popUpsContainer;

        private List<PopUp> _popUps = new();

        public void AttachPopUp(PopUp popUp)
        {
            _popUps.Add(popUp);
            popUp.transform.SetParent(_popUpsContainer, false);
        }

        public void RemovePopUp(PopUp popUp)
        {
            _popUps.Remove(popUp);
        }

        public void DestroyAllPopUps()
        {
            foreach (var popUp in _popUps)
            {
                Destroy(popUp.gameObject);
            }

            _popUps.Clear();
        }
    }
}