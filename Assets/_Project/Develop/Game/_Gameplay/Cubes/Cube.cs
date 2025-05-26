namespace Gameplay
{
    public class Cube
    {
        private readonly CubeView _view;

        private int _curretSideIndex;

        public Cube(CubeView view)
        {
            _view = view;

            _view.OnUnpressed.AddListener(() => RotateToNextSide());
        }

        private void RotateToNextSide()
        {
            _curretSideIndex = _curretSideIndex + 1 >= 10 ? 0 : _curretSideIndex + 1;

            _view.Rotate(_curretSideIndex);
        }
    }
}