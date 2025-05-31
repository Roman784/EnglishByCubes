using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class SlotBarFactory : PlaceholderFactory<SlotBarView, SlotBar>
    {
        private readonly DiContainer _container;
        private readonly SlotBarView _prefab;

        [Inject]
        public SlotBarFactory(DiContainer container, SlotBarView prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public SlotBar Create(Vector3 position)
        {
            var newSlotBar = Create();
            newSlotBar.SetPosition(position);

            return newSlotBar;
        }

        public SlotBar Create()
        {
            var view = Object.Instantiate(_prefab);
            var newSlotBar = _container.Instantiate<SlotBar>(new object[] { view });

            return newSlotBar;
        }
    }
}