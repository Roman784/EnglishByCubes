using System.Collections.Generic;
using Template;
using Theme;
using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TemplateConfigs", menuName = "Game Configs/Level/New Template Configs")]
    public class TemplateLevelConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.Template;

        [field: Space]

        [field: SerializeField] public List<TemplateSlotData> Slots { get; private set; }
        [field: SerializeField] public List<int> CubeNumbersPool { get; private set; }
    }
}