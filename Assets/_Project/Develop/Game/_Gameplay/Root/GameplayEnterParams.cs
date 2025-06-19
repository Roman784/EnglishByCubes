using GameRoot;

namespace Gameplay
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public readonly int Number;

        public GameplayEnterParams(int number) : base(Scenes.GAMEPLAY)
        {
            Number = number;
        }
    }
}