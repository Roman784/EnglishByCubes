using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProgressBarConfigs", menuName = "Game Configs/Theme/New Progress Bar Configs")]
    public class ProgressBarConfigs : ScriptableObject
    {
        [field: SerializeField] public float FillingDuration { get; private set; }
        [field: SerializeField] public Ease FillingEase { get; private set; }
        [field: SerializeField] public Gradient FillingGradient { get; private set; }
    }
}