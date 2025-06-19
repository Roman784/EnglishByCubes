using GameRoot;

namespace LevelMenu
{
    public class LevelMenuEnterParams : SceneEnterParams
    {
        public int CurrentLevelNumber { get; private set; }

        public LevelMenuEnterParams(string exitSceneName, int currentLevelNumber) : base(Scenes.LEVEL_MENU, exitSceneName)
        {
            CurrentLevelNumber = currentLevelNumber;
        }
    }
}