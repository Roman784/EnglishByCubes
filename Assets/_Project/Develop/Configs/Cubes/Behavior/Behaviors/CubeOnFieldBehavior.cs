using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class CubeOnFieldBehavior : CubeBehavior
    {
        private readonly Camera _camera;

        private Tween _wordListOpeningCountdown;

        public CubeOnFieldBehavior(CubeBehaviorHandler handler, Cube cube) : base(handler, cube)
        {
            _camera = Camera.main;
        }

        public override void Enter()
        {
        }

        public override void OnPressed()
        {
            var startMousePosition = GetMousePosition();

            _cube.Select();
            _cube.StartDragging();

            _wordListOpeningCountdown?.Kill(false);
            _wordListOpeningCountdown = DOVirtual.DelayedCall(0.5f, () => 
            {
                if (Vector3.Distance(startMousePosition, GetMousePosition()) > 0.25f) return;

                _cube.Deselect();
                _cube.StopDragging();
                _cube.OpenWordList();
            });
        }

        public override void OnUnpressed()
        {
            _cube.Deselect();
            _cube.StopDragging();

            if (_cube.Position.z < -3.25f)
            {
                _cube.RemoveFromField();
            }
            else
            {
                _cube.SetAccordingPreview();
            }
        }

        private Vector3 GetMousePosition()
        {
            var position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.y = _cube.Position.y;

            return position;
        }
    }
}