using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CollectionAudioConfigs", menuName = "Game Configs/Audio/New Collection Audio Configs")]
    public class CollectionAudioConfigs : ScriptableObject
    {
        [field: SerializeField] public AudioClip ItemFallSound { get; private set; }
    }
}