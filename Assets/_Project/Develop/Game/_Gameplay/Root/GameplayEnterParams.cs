using GameRoot;

namespace Gameplay
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public int LevelNumber { get; private set; }

        public GameplayEnterParams(string exitSceneName, int levelNumber) : base(Scenes.GAMEPLAY, exitSceneName)
        {
            LevelNumber = levelNumber;
        }
    }
}