using Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;

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

            _view.OnSwitched.AddListener(SwitchCubes);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }

        public void CreateCubes(IEnumerable<CubeConfigs> cubesConfigs)
        {
            foreach (CubeConfigs cubeConfigs in cubesConfigs)
            {
                CreateCube(cubeConfigs);
            }

            _firstCubeIndex = 0;
            PlaceCubes();
            SetDisplayOfSwitchButtons();
        }

        private void CreateCube(CubeConfigs configs)
        {
            var newCube = _cubeFactory.Create(configs);
            newCube.Disable(true);

            _cubes.Add(newCube);
        }

        private void PlaceCubes()
        {
            for (int i = 0; i < _cubes.Count; i++)
            {
                var cube = _cubes[i];

                if (i < _firstCubeIndex || i >= _firstCubeIndex + _slots.Count)
                    cube.Disable();
                else
                    cube.Enable();

                var slotIndex = i - _firstCubeIndex;
                if (slotIndex >= 0 && slotIndex < _slots.Count)
                {
                    _slots[slotIndex].PlaceCube(_cubes[i]);
                }
            }
        }

        private void SwitchCubes(int step)
        {
            _firstCubeIndex = Mathf.Clamp(_firstCubeIndex + step, 0, MaxFirstCubeIndex);

            PlaceCubes();
            SetDisplayOfSwitchButtons();
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