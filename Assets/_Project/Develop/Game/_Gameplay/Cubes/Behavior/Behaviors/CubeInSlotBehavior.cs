namespace Gameplay
{
    public class CubeInSlotBehavior : CubeBehavior
    {
        public CubeInSlotBehavior(CubeBehaviorHandler handler, Cube cube) : base(handler, cube)
        {
        }

        public override void Enter()
        {
        }

        public override void OnPointerUp()
        {
            _cube.CreateOnField();
        }

        public override void OnPointerEnter()
        {
            _cube.ShowName();
        }

        public override void OnPointerExit()
        {
            _cube.HideName();
        }
    }
}