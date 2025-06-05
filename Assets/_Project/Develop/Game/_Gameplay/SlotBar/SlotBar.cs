using Configs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Gameplay
{
    public class SlotBar
    {
        private readonly SlotBarView _view;

        private CubeFactory _cubeFactory;

        private List<Slot> _slots;
        private List<Cube> _cubes;

        private int _firstCubeIndex;

        private int MaxFirstCubeIndex => Mathf.Abs(_cubes.Count - _slots.Count) % _cubes.Count;

        [Inject]
        private void Construct(CubeFactory cubeFactory)
        {
            _cubeFactory = cubeFactory;
        }

        public SlotBar(SlotBarView view)
        {
            _view = view;

            _slots = _view.GetSlots();
            _cubes = new();
            _firstCubeIndex = 0;

            _view.OnSwitched.AddListener(SwitchCubes);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }

        public void CreateCubes(List<CubeConfigs> cubesConfigs)
        {
            for (int i = 0; i < cubesConfigs.Count(); i++)
            {
                var newCube = _cubeFactory.Create(cubesConfigs[i]);
                _cubes.Add(newCube);

                var slot = _slots[Mathf.Clamp(i, 0, _slots.Count - 1)];
                newCube.AddToSlot(slot);
            }

            PlaceCubes();
        }

        private void PlaceCubes()
        {
            for (int i = 0; i < _cubes.Count; i++)
            {
                var cube = _cubes[i];
                var slotIndex = i - _firstCubeIndex;

                if (slotIndex >= 0 && slotIndex < _slots.Count)
                {
                    cube.PlaceInSlot(_slots[slotIndex]);
                }
                else
                {
                    cube.DisableInSlots();
                }
            }

            SetDisplayOfSwitchButtons();
        }

        private void SwitchCubes(int step)
        {
            _firstCubeIndex = Mathf.Clamp(_firstCubeIndex + step, 0, MaxFirstCubeIndex);
            PlaceCubes();
        }

        private void SetDisplayOfSwitchButtons()
        {
            if (_cubes.Count <= _slots.Count)
            {
                _view.DisableLeftButton();
                _view.DisableRightButton();
            }
            else if (_firstCubeIndex == 0)
            {
                _view.DisableLeftButton();
                _view.EnableRightButton();
            }
            else if (_firstCubeIndex == _cubes.Count - _slots.Count)
            {
                _view.EnableLeftButton();
                _view.DisableRightButton();
            }
            else
            {
                _view.EnableLeftButton();
                _view.EnableRightButton();
            }
        }
    }
}