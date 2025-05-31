using UnityEngine;

namespace Gameplay
{
    public class SlotBar
    {
        private SlotBarView _view;

        public SlotBar(SlotBarView view)
        {
            _view = view;
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }
    }
}