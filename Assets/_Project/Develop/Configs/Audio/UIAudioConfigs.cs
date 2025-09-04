using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UIAudioConfigs", menuName = "Game Configs/Audio/New UI Audio Configs")]
    public class UIAudioConfigs : ScriptableObject
    {
        [field: SerializeField] public AudioClip ButtonClickSound { get; private set; }
        [field: SerializeField] public AudioClip LevelCompletionSound { get; private set; }
        [field: SerializeField] public AudioClip SentenceCompletedSound { get; private set; }
    }
}