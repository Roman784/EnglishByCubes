using Configs;
using System.Collections.Generic;
using Zenject;
using R3;
using UnityEngine;
using System.Linq;

namespace Gameplay
{
    public class GameFieldService
    {
        private CubeFactory _cubeFactory; 
        private CubesLayoutService _cubesLayoutService;

        private List<Cube> _cubes = new();

        private List<Cube> _previewCubes = new();
        private int _previewCubeIndex;
        private List<Cube> _movingPreviewCubes;

        [Inject]
        private void Construct(CubeFactory cubeFactory, CubesLayoutService cubesLayoutService)
        {
            _cubeFactory = cubeFactory;
            _cubesLayoutService = cubesLayoutService;
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
            if (_cubes.Contains(cube))
            {
                _cubes.Remove(cube);
                cube.Destroy().Subscribe(_ =>
                {
                    _cubesLayoutService.LayOut(_cubes);
                });
            }
        }

        public void PrepareForPreviewCubePosition(Cube cube)
        {
            _previewCubes = new List<Cube>(_cubes);
            _previewCubeIndex = _previewCubes.IndexOf(cube);
            _previewCubes[_previewCubeIndex] = null;
            _movingPreviewCubes = new();
        }

        public void PreviewCubePosition(Cube originCube)
        {
            if (_previewCubes.Count < 1) return;

            var cubeScale = _cubesLayoutService.GetCubeScale(_previewCubes.Count);

            for (int i = 0; i < _previewCubes.Count; i++)
            {
                var cube = _previewCubes[i];
                if (cube == null) continue;

                var distance = Vector3.Distance(cube.Position, originCube.Position);
                if (distance < cubeScale / 1.25f)
                {
                    if (_movingPreviewCubes.Contains(cube)) return;

                    _previewCubes.RemoveAt(_previewCubeIndex);
                    _previewCubes.Insert(i, null);
                    _previewCubeIndex = i;

                    _movingPreviewCubes.Add(cube);
                    _cubesLayoutService.LayOut(_previewCubes).Subscribe(_ =>
                    {
                        _movingPreviewCubes.Remove(cube);
                    });

                    return;
                }
            }
        }

        public void SwapCubesAccordingPreview()
        {
            var absentCube = _cubes.Except(_previewCubes).FirstOrDefault();
            _previewCubes[_previewCubes.IndexOf(null)] = absentCube;

            _cubes.Clear();
            _cubes.AddRange(_previewCubes);
            _cubes.RemoveAll(c => c == null);

            _cubesLayoutService.LayOut(_cubes);
        }
    }
}