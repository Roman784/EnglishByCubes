using GameRoot;

namespace MistakeCorrection
{
    public class MistakeCorrectionEnterParams : SceneEnterParams
    {
        public readonly int Number;

        public MistakeCorrectionEnterParams(int number) : base(Scenes.MISTAKE_CORRECTION)
        {
            Number = number;
        }
    }
}