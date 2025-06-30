using Gameplay;
using GameRoot;
using UnityEngine;
using Zenject;

namespace Template
{
    public class TemplateSlotsFactory : PlaceholderFactory<TemplateSlotsView, TemplateSlots>
    {
        private readonly DiContainer _container;
        private readonly TemplateSlotsView _prefab;
        private readonly TemplateSlot _slotPrefab;
        private readonly TemplateCubesLayoutService _cubesLayoutService;

        [Inject]
        public TemplateSlotsFactory(DiContainer container, TemplateSlotsView prefab, TemplateSlot slotPrefab,
                                    ICubesLayoutService cubesLayoutService)
        {
            _container = container;
            _prefab = prefab;
            _slotPrefab = slotPrefab;
            _cubesLayoutService = (TemplateCubesLayoutService)cubesLayoutService;
        }

        public TemplateSlots Create(Vector3 position)
        {
            var newSlots = Create();
            newSlots.SetPosition(position);

            return newSlots;
        }

        public TemplateSlots Create()
        {
            var view = Object.Instantiate(_prefab);
            var newSlots = _container.Instantiate<TemplateSlots>(new object[] { view, _slotPrefab, _cubesLayoutService });

            return newSlots;
        }
    }
}