using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubesConfigs", menuName = "Game Configs/Cubes/New Cubes Configs")]
    public class CubesConfigs : ScriptableObject
    {
        [field: SerializeField] public List<CubeConfigs> Cubes { get; private set; }

        public HashSet<CubeConfigs> GetCubes(params int[] numbers)
        {
            var cubes = new HashSet<CubeConfigs>();

            foreach (var cube in Cubes)
            {
                if (numbers.Contains(cube.Number))
                    cubes.Add(cube);
            }

            return cubes;
        }

        private void OnValidate()
        {
            ValidateCubesNumbers();
        }

        private void ValidateCubesNumbers()
        {
            for (int i = 0; i < Cubes.Count; i++)
            {
                var number = Cubes[i].Number;
                for (int j = i + 1; j < Cubes.Count; j++)
                {
                    if (number == Cubes[j].Number)
                        Debug.LogError($"Cube numbers are repeated: {number}. Indexes: {i} and {j}!");
                }
            }
        }
    }
}