using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PopUpConfigs", menuName = "Game Configs/UI/New PopUp Configs")]
    public class PopUpConfigs : ScriptableObject
    {
        [field: SerializeField] public float OpenDuration { get; private set; }
        [field: SerializeField] public Ease OpenEase { get; private set; }
        [field: SerializeField] public float CloseDuration { get; private set; }
        [field: SerializeField] public Ease CloseEase { get; private set; }
    }
}