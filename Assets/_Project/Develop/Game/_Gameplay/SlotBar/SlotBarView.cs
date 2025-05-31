using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class SlotBarView : MonoBehaviour
    {
        [SerializeField] private List<Slot> _slots;

        public List<Slot> GetSlots()
        {
            return _slots;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}