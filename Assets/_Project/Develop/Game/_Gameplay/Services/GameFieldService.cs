using Configs;
using System.Collections.Generic;
using Zenject;
using R3;
using UnityEngine;

namespace Gameplay
{
    public class GameFieldService
    {
        private CubeFactory _cubeFactory; 
        private CubesLayoutService _cubesLayoutService;

        private List<Cube> _cubes = new();

        [Inject]
        private void Construct(CubeFactory cubeFactory, CubesLayoutService cubesLayoutService)
        {
            _cubeFactory = cubeFactory;
            _cubesLayoutService = cubesLayoutService;
        }

        public void CreateCube(CubeConfigs configs)
        {
            var newCube = _cubeFactory.Create(configs);
            _cubes.Add(newCube);

            _cubesLayoutService.LayOut(_cubes);
        }

        public void RemoveCube(Cube cube)
        {
            if (_cubes.Contains(cube))
            {
                _cubes.Remove(cube);
                cube.Destroy().Subscribe(_ =>
                {
                    _cubesLayoutService.LayOut(_cubes);
                });
            }
        }

        public void CheckAndSwap(Cube originCube)
        {
            if (_cubes.Count == 1)
            {
                _cubesLayoutService.LayOut(_cubes);
                return;
            }

            var originCubeIndex = _cubes.IndexOf(originCube);
            var cubeScale = _cubesLayoutService.GetCubeScale(_cubes.Count);

            for (int i = 0; i < _cubes.Count; i++)
            {
                var cube = _cubes[i];
                var distance = Vector3.Distance(cube.Position, originCube.Position);

                if (distance < cubeScale / 1.5f && cube != originCube)
                {
                    _cubes.RemoveAt(originCubeIndex);
                    _cubes.Insert(i, originCube);
                    break;
                }
            }

            _cubesLayoutService.LayOut(_cubes);
        }
    }
}