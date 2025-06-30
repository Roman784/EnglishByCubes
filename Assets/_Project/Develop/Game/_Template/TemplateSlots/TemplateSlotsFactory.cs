using Gameplay;
using UnityEngine;
using Zenject;

namespace Template
{
    public class TemplateSlotsFactory : PlaceholderFactory<TemplateSlotsView, TemplateSlots>
    {
        private readonly DiContainer _container;
        private readonly TemplateSlotsView _prefab;

        [Inject]
        public TemplateSlotsFactory(DiContainer container, TemplateSlotsView prefab)
        {
            _container = container;
            _prefab = prefab;
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
            var newSlots = _container.Instantiate<TemplateSlots>(new object[] { view });

            return newSlots;
        }
    }
}