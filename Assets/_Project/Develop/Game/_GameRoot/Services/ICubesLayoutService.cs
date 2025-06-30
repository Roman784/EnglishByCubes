using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace GameRoot
{
    public interface ICubesLayoutService
    {
        public void LayOut(List<Cube> cubes);
        public Vector3 GetLastCubePosition(int cubesCount);
        public List<Vector3> GetCubePositions(int cubesCount);
        public float GetCubeScale(int cubesCount);
    }
}