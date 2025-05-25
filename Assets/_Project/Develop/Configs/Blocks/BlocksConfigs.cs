using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BlocksConfigs", menuName = "Game Configs/Blocks/New Blocks Configs")]
    public class BlocksConfigs : ScriptableObject
    {
        [field: SerializeField] public List<BlockConfigs> Blocks { get; private set; }

        private void OnValidate()
        {
            ValidateBlocksNumbers();
        }

        private void ValidateBlocksNumbers()
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                var number = Blocks[i].Number;
                for (int j = i + 1; j < Blocks.Count; j++)
                {
                    if (number == Blocks[j].Number)
                        Debug.LogError($"Block numbers are repeated: {number}. Indexes: {i} and {j}!");
                }
            }
        }
    }
}