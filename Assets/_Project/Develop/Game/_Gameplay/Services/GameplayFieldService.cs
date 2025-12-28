using Configs;
using System.Collections.Generic;
using Zenject;
using R3;
using UnityEngine;
using UnityEngine.Events;
using GameRoot;

namespace Gameplay
{
    public class GameplayFieldService : IGameFieldService
    {
        private List<Cube> _cubes = new();

        private CubeFactory _cubeFactory;
        private ICubesLayoutService _cubesLayoutService;
        private CubesPositionPreviewService _cubesPositionPreviewService;
        private ILevelPassingService _levelPassingService;

        public IReadOnlyList<Cube> Cubes => _cubes;
        public UnityEvent<Cube> OnCubeCreated { get; private set; } = new();

        [Inject]
        private void Construct(CubeFactory cubeFactory,
                               CubesPositionPreviewService cubesPositionPreviewService,
                               ICubesLayoutService cubesLayoutService,
                               ILevelPassingService levelPassingService)
        {
            _cubeFactory = cubeFactory;
            _cubesLayoutService = cubesLayoutService;
            _cubesPositionPreviewService = cubesPositionPreviewService;
            _levelPassingService = levelPassingService;
        }

        public Cube CreateCube(CubeConfigs configs, int side = 0)
        {
            var position = _cubesLayoutService.GetLastCubePosition(_cubes.Count + 1);

            var newCube = _cubeFactory.Create(configs, position, side);
            newCube.DisableOnField();

            _cubes.Add(newCube);

            CalculateSentenceMatching();
            newCube.OnRotated.AddListener(CalculateSentenceMatching);

            _cubesLayoutService.LayOut(_cubes);

            OnCubeCreated.Invoke(newCube);

            return newCube;
        }

        public void RemoveCube(Cube cube)
        {
            if (!_cubes.Contains(cube)) return;
            
            _cubes.Remove(cube);
            CalculateSentenceMatching();

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

            CalculateSentenceMatching();
            _cubesLayoutService.LayOut(_cubes);
        }

        private void CalculateSentenceMatching()
        {
            _levelPassingService.CalculateSentenceMatching(MakeSentence());
        }

        private string MakeSentence()
        {
            var sentence = "";
            foreach (var cube in _cubes)
            {
                sentence += cube.CurrentWord;
            }

            return sentence;
        }
    }
}