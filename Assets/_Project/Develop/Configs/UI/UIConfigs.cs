using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UIConfigs", menuName = "Game Configs/UI/New UI Configs")]
    public class UIConfigs : ScriptableObject
    {
        [field: SerializeField] public PopUpConfigs PopUpConfigs { get; private set; }
        [field: SerializeField] public ProgressBarConfigs GameplayProgressBarConfigs { get; private set; }
    }
}