using System;
using System.Collections.Generic;

namespace GameState
{
    [Serializable]
    public class GameState
    {
        public int LastCompletedLevelNumber;
        public int CurrentThemeMode;
        public List<int> CollectedCollectionItems;
        public float CurrentCollectionItemFill;
    }
}