using Configs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using Audio;

namespace Gameplay
{
    public class SlotBar
    {
        private readonly SlotBarView _view;

        private CubeFactory _cubeFactory;

        private List<Slot> _slots;
        private List<Cube> _cubes;
        private List<(int, Cube)> _removedCubesMap;

        private int _firstCubeIndex;

        private int MaxFirstCubeIndex => Mathf.Abs(_cubes.Count - _slots.Count) % _cubes.Count;

        public SlotBar(SlotBarView view, CubeFactory cubeFactory, 
                       AudioProvider audioProvider, IConfigsProvider configsProvider)
        {
            _view = view;
            _cubeFactory = cubeFactory;

            var switchSound = configsProvider.GameConfigs.AudioConfigs.UIConfigs.ButtonClickSound;

            _slots = _view.GetSlots();
            _cubes = new();
            _removedCubesMap = new();
            _firstCubeIndex = 0;

            _view.Init(audioProvider, switchSound);
            _view.OnSwitched.AddListener(SwitchCubes);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }

        public void CreateCubes(List<CubeConfigs> cubesConfigs)
        {
            _cubes = new();
            _cubes.Capacity = cubesConfigs.Count;

            for (int i = 0; i < cubesConfigs.Count(); i++)
            {
                var newCube = _cubeFactory.Create(cubesConfigs[i]);
                AddCube(newCube, i, i);
            }

            PlaceCubes();
        }

        public void AddCube(Cube newCube, int cubePos, int slotIdx)
        {
            _cubes.Insert(cubePos, newCube);

            slotIdx = Mathf.Clamp(slotIdx, 0, _slots.Count - 1);
            var slot = _slots[slotIdx];

            newCube.AddToSlot(this, slot);
        }

        public void RemoveCube(Cube cube)
        {
            _removedCubesMap.Add((cube.Number, cube));

            _cubes.Remove(cube);
            cube.DisableInSlots();

            SwitchCubes(-1);
        }

        public void RestoreCube(int cubeNumber)
        {
            foreach (var cubeItem in _removedCubesMap)
            {
                if (cubeNumber != cubeItem.Item1) continue;

                AddCube(cubeItem.Item2, _firstCubeIndex, 0);
                PlaceCubes();

                _removedCubesMap.Remove(cubeItem);
                return;
            }
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
            if (_cubes.Count == 0) return;

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