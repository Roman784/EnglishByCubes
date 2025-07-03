using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CollectionAudioConfigs", menuName = "Game Configs/Audio/New Collection Audio Configs")]
    public class CollectionAudioConfigs : ScriptableObject
    {
        [field: SerializeField] public AudioClip ItemFallSound { get; private set; }
        [field: SerializeField] public AudioClip ItemFillingSound { get; private set; }
        [field: SerializeField] public AudioClip NewItemUnlockSound { get; private set; }
    }
}