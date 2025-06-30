using UnityEngine;

namespace Template
{
    public class TemplateSlots
    {
        private readonly TemplateSlotsView _view;

        public TemplateSlots(TemplateSlotsView view)
        {
            _view = view;
        }

        public void SetPosition(Vector3 position) => _view.SetPosition(position);
    }
}