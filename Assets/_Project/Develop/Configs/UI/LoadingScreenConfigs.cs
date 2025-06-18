using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LoadingScreenConfigs", menuName = "Game Configs/UI/New Loading Screen Configs")]
    public class LoadingScreenConfigs : ScriptableObject
    {
        [field: SerializeField] public float ShowingDuration { get; private set; }
        [field: SerializeField] public Ease ShowingEase { get; private set; }

        [field: Space]

        [field: SerializeField] public float HidingDuration { get; private set; }
        [field: SerializeField] public Ease HidingEase { get; private set; }
    }
}