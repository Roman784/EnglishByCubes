using GameRoot;

namespace LevelMenu
{
    public class LevelMenuEnterParams : SceneEnterParams
    {
        public LevelMenuEnterParams(string exitSceneName) : base(Scenes.LEVEL_MENU, exitSceneName)
        {
        }
    }
}