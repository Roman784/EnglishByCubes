using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubeConfigs", menuName = "Game Configs/Cubes/New Cube Configs")]
    public class CubeConfigs : ScriptableObject
    {
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public List<string> Words { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }

        [field: Space]

        [field: SerializeField] public CubeDataConfigs DataConfigs { get; private set; }
    }
}