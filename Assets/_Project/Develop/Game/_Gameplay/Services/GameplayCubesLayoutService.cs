using Configs;
using GameRoot;
using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayCubesLayoutService : ICubesLayoutService
    {
        private IConfigsProvider _configsProvider;

        private CubesLayoutConfigs Configs => _configsProvider.GameConfigs.CubesConfigs.CubesLayoutConfigs;
        private float CubeSpacing => Configs.Spacing;

        [Inject]
        private void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void LayOut(List<Cube> cubes)
        {
            var positions = GetCubePositions(cubes.Count);
            var scale = GetCubeScale(cubes.Count);

            for (int i = 0; i < cubes.Count; i++)
            {
                var cube = cubes[i];
                if (cube == null) continue;

                var position = positions[i];
                cube.PlaceOnField(position, scale);
            }
        }

        public Vector3 GetLastCubePosition(int cubesCount)
        {
            return GetCubePositions(cubesCount)[cubesCount - 1];
        }

        public List<Vector3> GetCubePositions(int cubesCount)
        {
            var positions = new List<Vector3>();

            var cubeScale = GetCubeScale(cubesCount);

            var totalWidth = cubesCount * cubeScale + (cubesCount - 1) * CubeSpacing;
            var startX = (-totalWidth + cubeScale) / 2f;

            for (int i = 0; i < cubesCount; i++)
            {
                var position = Vector3.right * (startX + i * (cubeScale + CubeSpacing));
                positions.Add(position);
            }

            return positions;
        }

        public float GetCubeScale(int cubesCount)
        {
            var camera = Camera.main;
            var totalScreenWidth = camera.orthographicSize * camera.aspect * 2f;

            var scale = (totalScreenWidth - (cubesCount + 1) * CubeSpacing) / cubesCount;
            scale = Mathf.Clamp01(scale);

            return scale;
        }
    }
}