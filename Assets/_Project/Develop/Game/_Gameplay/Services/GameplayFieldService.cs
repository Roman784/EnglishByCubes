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
        private CubesLayoutService _cubesLayoutService;
        private CubesPositionPreviewService _cubesPositionPreviewService;
        private TaskPassingService _taskPassingService;

        public IReadOnlyList<Cube> Cubes => _cubes;
        public UnityEvent<Cube> OnCubeCreated { get; private set; } = new();

        [Inject]
        private void Construct(CubeFactory cubeFactory,
                               CubesPositionPreviewService cubesPositionPreviewService,
                               CubesLayoutService cubesLayoutService,
                               TaskPassingService taskPassingService)
        {
            _cubeFactory = cubeFactory;
            _cubesLayoutService = cubesLayoutService;
            _cubesPositionPreviewService = cubesPositionPreviewService;
            _taskPassingService = taskPassingService;
        }

        public void CreateCube(CubeConfigs configs)
        {
            var position = _cubesLayoutService.GetLastCubePosition(_cubes.Count + 1);

            var newCube = _cubeFactory.Create(configs, position);
            newCube.DisableOnField();

            _cubes.Add(newCube);

            CalculateSentenceMatching();
            newCube.OnRotated.AddListener(CalculateSentenceMatching);

            _cubesLayoutService.LayOut(_cubes);

            OnCubeCreated.Invoke(newCube);
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
            _taskPassingService.CalculateSentenceMatching(MakeSentence());
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