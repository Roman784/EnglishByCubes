using Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;
using R3;
using DG.Tweening;
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
                
                newCube.Disable(true);
                if (i < _slots.Count)
                    newCube.Enable();

                var slotIndex = i < _slots.Count ? i : _slots.Count - 1;
                _slots[slotIndex].PlaceCube(newCube);

                _cubes.Add(newCube);
            }

            SetDisplayOfSwitchButtons();
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
                    _slots[slotIndex].PlaceCube(_cubes[i], 0.25f, Ease.OutQuad);
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