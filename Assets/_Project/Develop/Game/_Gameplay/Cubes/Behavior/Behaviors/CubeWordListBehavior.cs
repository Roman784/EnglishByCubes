namespace Gameplay
{
    public class CubeWordListBehavior : CubeBehavior
    {
        private bool _isPressed;

        public CubeWordListBehavior(CubeBehaviorHandler handler, Cube cube) : base(handler, cube)
        {
        }

        public override void Enter()
        {
            _isPressed = false;

            _cube.Deselect();
        }

        public override void OnPointerDown()
        {
            _isPressed = true;
        }

        public override void OnPointerUp()
        {
            if (!_isPressed) return;

            _cube.CloseWordList();
            _handler.SetOnFieldBehavior();
        }
    }
}