using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CubesLayoutService
    {
        public Observable<bool> LayOut(List<Cube> cubes)
        {
            Observable<bool> onCompleted = null;

            var positions = GetCubePositions(cubes.Count);
            var scale = GetCubeScale(cubes.Count);

            for (int i = 0; i < cubes.Count; i++)
            {
                var cube = cubes[i];
                if (cube == null) continue;

                var position = positions[i];
                onCompleted = cube.PlaceOnField(position, scale);
            }

            return onCompleted ?? Observable.Return(true);
        }

        public Vector3 GetLastCubePosition(int cubesCount)
        {
            return GetCubePositions(cubesCount)[cubesCount - 1];
        }

        public List<Vector3> GetCubePositions(int cubesCount)
        {
            var positions = new List<Vector3>();
            var cubeSpacing = 0.2f;

            var cubeScale = GetCubeScale(cubesCount);

            var totalWidth = cubesCount * cubeScale + (cubesCount - 1) * cubeSpacing;
            var startX = (-totalWidth + cubeScale) / 2f;

            for (int i = 0; i < cubesCount; i++)
            {
                var position = Vector3.right * (startX + i * (cubeScale + cubeSpacing));
                positions.Add(position);
            }

            return positions;
        }

        public float GetCubeScale(int cubesCount)
        {
            var cubeSpacing = 0.2f;
            var camera = Camera.main;
            var totalScreenWidth = camera.orthographicSize * camera.aspect * 2f;

            var scale = (totalScreenWidth - (cubesCount + 1) * cubeSpacing) / cubesCount;
            scale = Mathf.Clamp01(scale);

            return scale;
        }
    }
}