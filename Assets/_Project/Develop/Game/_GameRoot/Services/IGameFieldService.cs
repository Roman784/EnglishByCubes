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

        public Cube CreateCube(CubeConfigs configs, int side = 0);
        public void RemoveCube(Cube cube);
        public void SetCubesAccordingPreview();
    }
}