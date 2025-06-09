using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class CubeOnFieldBehavior : CubeBehavior
    {
        private readonly Camera _camera;

        private bool _isPressed;
        private Vector3 _startMousePosition;
        private Tween _wordListOpeningCountdown;
        private float _startTime;

        public CubeOnFieldBehavior(CubeBehaviorHandler handler, Cube cube) : base(handler, cube)
        {
            _camera = Camera.main;
        }

        public override void Enter()
        {
            _isPressed = false;

            _cube.CloseWordList();
        }

        public override void OnPointerDown()
        {
            _isPressed = true;
            _startMousePosition = GetMousePosition();
            _startTime = Time.time;

            _cube.Select();
            _cube.StartDragging();

            _wordListOpeningCountdown?.Kill(false);
            _wordListOpeningCountdown = DOVirtual.DelayedCall(0.5f, () => 
            {
                if (Vector3.Distance(_startMousePosition, GetMousePosition()) > 0.25f || !_isPressed) return;

                _cube.StopDragging();
                _cube.SetAccordingPreview();

                _cube.OpenWordList();
            });
        }

        public override void OnPointerUp()
        {
            _isPressed = false;

            _cube.Deselect();
            _cube.StopDragging();

            if (Vector3.Distance(_startMousePosition, GetMousePosition()) < 0.25f &&
                Time.time - _startTime < 0.5f)
            {
                _cube.RotateToNextSide();
            }

            if (_cube.Position.z < -3.25f)
            {
                _cube.RemoveFromField();
            }
            else
            {
                _cube.SetAccordingPreview();
            }
        }

        public override void OnPointerEnter()
        {
            
        }

        public override void OnPointerExit()
        {
            
        }

        private Vector3 GetMousePosition()
        {
            var position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.y = _cube.Position.y;

            return position;
        }
    }
}