using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BlockConfigs", menuName = "Game Configs/Blocks/New Block Configs")]
    public class BlockConfigs : ScriptableObject
    {
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public List<string> Sides { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}