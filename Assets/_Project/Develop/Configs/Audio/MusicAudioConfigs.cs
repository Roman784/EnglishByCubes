using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MusicAudioConfigs", menuName = "Game Configs/Audio/New Music Audio Configs")]
    public class MusicAudioConfigs : ScriptableObject
    {
        [field: SerializeField] public List<AudioClip> Clips { get; private set; }
    }
}