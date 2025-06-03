using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CubesLayoutService
    {
        public void LayOut(List<Cube> cubes)
        {
            var camera = Camera.main;
            var totalScreenWidth = camera.orthographicSize * camera.aspect * 2f;

            var cubeSpacing = 0.2f;

            var cubeScale = (totalScreenWidth - (cubes.Count + 1) * cubeSpacing) / cubes.Count;
            cubeScale = Mathf.Clamp01(cubeScale);

            var totalWidth = cubes.Count * cubeScale + (cubes.Count - 1) * cubeSpacing;
            var startX = (-totalWidth + cubeScale) / 2f;

            for (int i = 0; i < cubes.Count; i++)
            {
                var cube = cubes[i];
                var position = Vector3.right * (startX + i * (cubeScale + cubeSpacing));

                if (i == cubes.Count - 1) 
                    cube.SetPosition(position);

                cube.PlaceOnField(position, cubeScale);
            }
        }
    }
}