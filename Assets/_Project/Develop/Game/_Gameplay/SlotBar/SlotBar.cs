using Configs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class SlotBar
    {
        private readonly SlotBarView _view;

        private CubeFactory _cubeFactory;

        private List<Slot> _slots;
        private List<Cube> _cubes;

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
        }

        public void CreateCubes(IEnumerable<CubeConfigs> cubesConfigs)
        {
            foreach (CubeConfigs cubeConfigs in cubesConfigs)
            {
                CreateCube(cubeConfigs);
            }
        }

        private void CreateCube(CubeConfigs configs)
        {
            var newCube = _cubeFactory.Create(configs);
            newCube.Disable(true);

            _cubes.Add(newCube);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }
    }
}