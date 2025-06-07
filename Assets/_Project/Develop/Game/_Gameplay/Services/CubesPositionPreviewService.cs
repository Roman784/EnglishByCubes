using System.Collections.Generic;
using UnityEngine;
using Zenject;
using R3;
using System.Linq;

namespace Gameplay
{ 
    public class CubesPositionPreviewService
    {
        private List<Cube> _cubes;
        private Cube _draggedCube;
        private int _draggedCubeIndex;
        private List<Cube> _movingCubes;

        private GameFieldService _gameFieldService;
        private CubesLayoutService _cubesLayoutService;

        public List<Cube> Cubes
        {
            get
            {
                if (_cubes == null)
                    return new List<Cube>(_gameFieldService.Cubes);

                return _cubes.Select(c => c ?? _draggedCube).ToList();
            }
        }

        [Inject]
        private void Construct(GameFieldService gameFieldService, CubesLayoutService cubesLayoutService)
        {
            _gameFieldService = gameFieldService;
            _cubesLayoutService = cubesLayoutService;
        }

        public void PrepareForPreviewCubePosition(Cube cube)
        {
            _cubes = new List<Cube>(_gameFieldService.Cubes);
            _draggedCube = cube;
            _draggedCubeIndex = _cubes.IndexOf(cube);
            _cubes[_draggedCubeIndex] = null;
            _movingCubes = new();
        }

        public void PreviewCubePosition(Cube originCube)
        {
            if (_cubes.Count < 1) return;

            var cubeScale = _cubesLayoutService.GetCubeScale(_cubes.Count);

            for (int i = 0; i < _cubes.Count; i++)
            {
                var cube = _cubes[i];
                if (cube == null) continue;

                var distance = Vector3.Distance(cube.Position, originCube.Position);
                if (distance < cubeScale / 1.25f)
                {
                    if (_movingCubes.Contains(cube)) return;

                    _cubes.RemoveAt(_draggedCubeIndex);
                    _cubes.Insert(i, null);
                    _draggedCubeIndex = i;

                    _movingCubes.Add(cube);
                    _cubesLayoutService.LayOut(_cubes).Subscribe(_ =>
                    {
                        _movingCubes.Remove(cube);
                    });

                    return;
                }
            }
        }
    }
}