using Configs;
using System.Collections.Generic;
using Zenject;
using R3;

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
            var newCube = _cubeFactory.Create(configs, true);
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
    }
}