using System;
using System.Collections.Generic;
using Theory;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TheoryPageConfigs", menuName = "Game Configs/Level/New Theory Page Configs")]
    public class TheoryPageConfigs : ScriptableObject
    {
        [field: SerializeField] public List<TheoryContent> Contents { get; private set; }
    }
}