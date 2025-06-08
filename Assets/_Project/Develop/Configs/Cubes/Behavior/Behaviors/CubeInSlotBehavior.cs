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

        public override void OnUnpressed()
        {
            _cube.CreateOnField();
        }
    }
}