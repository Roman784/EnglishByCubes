using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubeConfigs", menuName = "Game Configs/Cubes/New Cube Configs")]
    public class CubeConfigs : ScriptableObject
    {
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public List<string> Sides { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}