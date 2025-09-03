using Configs;
using Gameplay;
using GameRoot;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MistakeCorrection
{
    public class MistakeCorrectionFieldService : IGameFieldService
    {
        public IReadOnlyList<Cube> Cubes => throw new System.NotImplementedException();

        public UnityEvent<Cube> OnCubeCreated => throw new System.NotImplementedException();

        public Cube CreateCube(CubeConfigs configs)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCube(Cube cube)
        {
            throw new System.NotImplementedException();
        }

        public void SetCubesAccordingPreview()
        {
            throw new System.NotImplementedException();
        }
    }
}