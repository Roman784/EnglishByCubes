using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubeRemoveAreaConfigs", menuName = "Game Configs/New Cube Remove Area Configs")]
    public class CubeRemoveAreaConfigs : ScriptableObject
    {
        [field: SerializeField] public float FirstHighlightLevel { get; private set; }
        [field: SerializeField] public float SecondHighlightLevel { get; private set; }
        [field: SerializeField] public float HighlightDuration { get; private set; }
        [field: SerializeField] public Ease HighlightEase { get; private set; }
    }
}