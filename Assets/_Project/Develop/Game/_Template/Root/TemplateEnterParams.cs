using GameRoot;

namespace Template
{
    public class TemplateEnterParams : SceneEnterParams
    {
        public readonly int Number;

        public TemplateEnterParams(int number) : base(Scenes.TEMPLATE)
        {
            Number = number;
        }
    }
}