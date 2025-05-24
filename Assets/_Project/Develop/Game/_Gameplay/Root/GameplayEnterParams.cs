using GameRoot;

namespace Gameplay
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public GameplayEnterParams(string exitSceneName) : base(Scenes.GAMEPLAY, exitSceneName)
        {
        }
    }
}