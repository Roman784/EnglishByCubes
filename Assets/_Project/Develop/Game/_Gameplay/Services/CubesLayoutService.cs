using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CubesLayoutService
    {
        public void LayOut(List<Cube> cubes)
        {
            var cubeSpacing = 0.2f;

            var cubeScale = GetCubeScale(cubes.Count);
            cubeScale = Mathf.Clamp01(cubeScale);

            var totalWidth = cubes.Count * cubeScale + (cubes.Count - 1) * cubeSpacing;
            var startX = (-totalWidth + cubeScale) / 2f;

            for (int i = 0; i < cubes.Count; i++)
            {
                var cube = cubes[i];
                var position = Vector3.right * (startX + i * (cubeScale + cubeSpacing));

                cube.PlaceOnField(position, cubeScale);
            }
        }

        public float GetCubeScale(int cubeCount)
        {
            var camera = Camera.main;
            var totalScreenWidth = camera.orthographicSize * camera.aspect * 2f;

            return (totalScreenWidth - (cubeCount + 1) * 0.2f) / cubeCount;
        }
    }
}