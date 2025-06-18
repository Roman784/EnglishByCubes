using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UIConfigs", menuName = "Game Configs/UI/New UI Configs")]
    public class UIConfigs : ScriptableObject
    {
        [field: SerializeField] public LoadingScreenConfigs LoadingScreenConfigs { get; private set; }
        [field: SerializeField] public PopUpConfigs PopUpConfigs { get; private set; }
        [field: SerializeField] public ProgressBarConfigs GameplayProgressBarConfigs { get; private set; }
        [field: SerializeField] public CubeRemoveAreaConfigs CubeRemoveAreaConfigs { get; private set; }
        [field: SerializeField] public LevelButtonsConfigs LevelButtonsConfigs { get; private set; }
    }
}