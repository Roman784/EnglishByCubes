using Configs;
using Gameplay;
using System.Collections.Generic;
using UnityEngine.Events;

namespace GameRoot
{
    public interface IGameFieldService
    {
        public IReadOnlyList<Cube> Cubes { get; }
        public UnityEvent<Cube> OnCubeCreated { get; }

        public void CreateCube(CubeConfigs configs);
        public void RemoveCube(Cube cube);
        public void SetCubesAccordingPreview();
    }
}