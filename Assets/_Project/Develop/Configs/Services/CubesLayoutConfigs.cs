using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubesLayoutConfigs", menuName = "Game Configs/Services/New Cubes Layout Configs")]
    public class CubesLayoutConfigs : ScriptableObject
    {
        [field: SerializeField] public float Spacing { get; private set; }
    }
}