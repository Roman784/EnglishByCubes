using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Template
{
    public class TemplateSlots
    {
        private readonly TemplateSlotsView _view;
        private readonly TemplateSlot _slotPrefab;
        private readonly TemplateCubesLayoutService _cubesLayoutService;

        private List<TemplateSlot> _slots = new();

        public TemplateSlots(TemplateSlotsView view, TemplateSlot slotPrefab,
                             TemplateCubesLayoutService cubesLayoutService)
        {
            _view = view;
            _slotPrefab = slotPrefab;
            _cubesLayoutService = cubesLayoutService;

            _cubesLayoutService.SetSlots(this);
        }

        public void SetPosition(Vector3 position) => _view.SetPosition(position);

        public void CreateSlots(List<TemplateSlotData> slotsData)
        {
            for (int i = 0; i < slotsData.Count; i++)
            {
                var data = slotsData[i];
                var isFirstPart = i == 0;
                var isLastPart = i == slotsData.Count - 1;

                var newSlot = Object.Instantiate(_slotPrefab);
                newSlot.Init(data, isFirstPart, isLastPart);

                _slots.Add(newSlot);
                _view.AddSlot(newSlot);
            }

            _view.SetContainerCellSize(_slots.Count);
        }

        public List<Vector3> GetSlotPositions()
        {
            var positions = new List<Vector3>();
            foreach (var slot in _slots)
            {
                positions.Add(slot.Position);
            }

            return positions;
        }
    }
}