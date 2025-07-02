using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubeAudioConfigs", menuName = "Game Configs/Audio/New Cube Audio Configs")]
    public class CubeAudioConfigs : ScriptableObject
    {
        [field: SerializeField] public List<AudioClip> RotationSounds { get; private set; }
    }
}