using DG.Tweening;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelButtonsConfigs", menuName = "Game Configs/UI/New Level Buttons Configs")]
    public class LevelButtonsConfigs : ScriptableObject
    {
        [field: SerializeField] public float ContainerAdditionalHeight { get; private set; }

        [field: Space]

        [field: SerializeField] public Vector2 StartPosition { get; private set; }
        [field: SerializeField] public float Spacing { get; private set; }
        [field: SerializeField] public float Frequence { get; private set; }
        [field: SerializeField] public float Amplitude { get; private set; }

        [field: Space]

        [field: SerializeField] public float ScrollingDuration { get; private set; }
        [field: SerializeField] public Ease ScrollingEase { get; private set; }

        [field: Space]

        [field: SerializeField] public Sprite TheoryIcon { get; private set; }
        [field: SerializeField] public Sprite TemplateIcon { get; private set; }
        [field: SerializeField] public Sprite PracticeIcon { get; private set; }
    }
}