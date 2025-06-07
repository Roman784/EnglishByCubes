using Configs;
using System.Collections.Generic;
using Zenject;
using R3;

namespace Gameplay
{
    public class GameFieldService
    {
        private List<Cube> _cubes = new();

        private CubeFactory _cubeFactory;
        private CubesLayoutService _cubesLayoutService;
        private CubesPositionPreviewService _cubesPositionPreviewService;

        public IReadOnlyList<Cube> Cubes => _cubes;

        [Inject]
        private void Construct(CubeFactory cubeFactory,
                               CubesPositionPreviewService cubesPositionPreviewService,
                               CubesLayoutService cubesLayoutService)
        {
            _cubeFactory = cubeFactory;
            _cubesLayoutService = cubesLayoutService;
            _cubesPositionPreviewService = cubesPositionPreviewService;
        }

        public void CreateCube(CubeConfigs configs)
        {
            var position = _cubesLayoutService.GetLastCubePosition(_cubes.Count + 1);

            var newCube = _cubeFactory.Create(configs, position);
            newCube.DisableOnField();

            _cubes.Add(newCube);
            _cubesLayoutService.LayOut(_cubes);
        }

        public void RemoveCube(Cube cube)
        {
            if (!_cubes.Contains(cube)) return;
            
            _cubes.Remove(cube);
            cube.Destroy().Subscribe(_ =>
            {
                _cubesLayoutService.LayOut(_cubes);
            });
        }

        public void SetCubesAccordingPreview()
        {
            var previewCubes = _cubesPositionPreviewService.Cubes;

            _cubes.Clear();
            _cubes.AddRange(previewCubes);
            _cubes.RemoveAll(c => c == null);

            _cubesLayoutService.LayOut(_cubes);
        }
    }
}