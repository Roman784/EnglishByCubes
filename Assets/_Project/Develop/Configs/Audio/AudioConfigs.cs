using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AudioConfigs", menuName = "Game Configs/Audio/New Audio Configs")]
    public class AudioConfigs : ScriptableObject
    {
        [field: SerializeField] public UIAudioConfigs UIConfigs { get; private set; }
    }
}