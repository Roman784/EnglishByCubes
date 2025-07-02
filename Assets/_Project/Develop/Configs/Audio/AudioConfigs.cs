using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AudioConfigs", menuName = "Game Configs/Audio/New Audio Configs")]
    public class AudioConfigs : ScriptableObject
    {
        [field: SerializeField] public MusicAudioConfigs MusicConfigs { get; private set; }
        [field: SerializeField] public UIAudioConfigs UIConfigs { get; private set; }
        [field: SerializeField] public CubeAudioConfigs CubeConfigs { get; private set; }
    }
}