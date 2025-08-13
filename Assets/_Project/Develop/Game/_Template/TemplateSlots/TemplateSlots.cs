using Configs;
using Gameplay;
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
        private readonly CubeFactory _cubeFactory;

        private List<TemplateSlot> _slots = new();

        public int Count => _slots.Count;

        public TemplateSlots(TemplateSlotsView view, TemplateSlot slotPrefab,
                             TemplateCubesLayoutService cubesLayoutService, CubeFactory cubeFactory)
        {
            _view = view;
            _slotPrefab = slotPrefab;
            _cubesLayoutService = cubesLayoutService;
            _cubeFactory = cubeFactory;

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

        public void CreateCubes(List<CubeConfigs> cubeConfigs)
        {
            foreach (var config in cubeConfigs)
            {
                var cube = _cubeFactory.Create(config);
                cube.CreateOnField();
                cube.DisableInSlots();
            }
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