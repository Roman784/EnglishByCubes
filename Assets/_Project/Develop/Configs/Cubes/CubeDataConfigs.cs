using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubeDataConfigs", menuName = "Game Configs/Cubes/New Cube Data Configs")]
    public class CubeDataConfigs : ScriptableObject
    {
        [field: SerializeField] public float RotationDuration { get; private set; }
        [field: SerializeField] public Ease RotationEase { get; private set; }

        [field: SerializeField] public float FadeDuration { get; private set; }
        [field: SerializeField] public Ease FadeEase { get; private set; }

        [field: SerializeField] public float RescaleDuration { get; private set; }
        [field: SerializeField] public Ease RescaleEase { get; private set; }

        [field: SerializeField] public float SwitchingInSlotsDuration { get; private set; }
        [field: SerializeField] public Ease SwitchingInSlotsEase { get; private set; }

        [field: SerializeField] public float FieldPlacementDuration { get; private set; }
        [field: SerializeField] public Ease FieldPlacementEase { get; private set; }
    }
}