using Theme;
using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TemplateConfigs", menuName = "Game Configs/Level/New Template Configs")]
    public class TemplateLevelConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.Template;
    }
}