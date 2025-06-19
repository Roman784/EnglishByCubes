using GameRoot;

namespace Theory
{
    public class TheoryEnterParams : SceneEnterParams
    {
        public readonly int Number;

        public TheoryEnterParams(int number) : base(Scenes.THEORY)
        {
            Number = number;
        }
    }
}