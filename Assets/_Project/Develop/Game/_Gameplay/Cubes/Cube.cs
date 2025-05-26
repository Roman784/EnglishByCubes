namespace Gameplay
{
    public class Cube
    {
        private readonly CubeView _view;

        public Cube(CubeView view)
        {
            _view = view;

            _view.OnUnpressed.AddListener(() => RotateToNextSide());
        }

        private void RotateToNextSide()
        {

        }
    }
}