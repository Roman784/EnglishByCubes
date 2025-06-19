using GameRoot;

namespace Theory
{
    public class TheoryEnterParams : SceneEnterParams
    {
        public readonly int LevelNumber;

        public TheoryEnterParams(string exitSceneName, int levelNumber) : base(Scenes.THEORY, exitSceneName)
        {
            LevelNumber = levelNumber;
        }
    }
}