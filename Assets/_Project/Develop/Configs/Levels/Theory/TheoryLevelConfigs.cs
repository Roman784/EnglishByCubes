using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TheoryConfigs", menuName = "Game Configs/Level/New Theory Configs")]
    public class TheoryLevelConfigs : LevelConfigs
    {
        public override LevelMode Mode => LevelMode.Theory;

        [field: Space]

        [field: SerializeField] public List<TheoryPageConfigs> PagesConfigs { get; private set; }
    }
}