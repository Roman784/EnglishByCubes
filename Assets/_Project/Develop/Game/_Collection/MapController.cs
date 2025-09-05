using Pause;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Collection
{
    public class MapController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPaused
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _dragStep;
        [SerializeField] private Vector4 _positionBounds;

        [Space]

        [SerializeField] private float _zoomSpeed;
        [SerializeField] private float _pinchZoomSpeed;
        [SerializeField] private Vector2 _zoomBoundsForVert;
        [SerializeField] private Vector2 _zoomBoundsForHor;

        [Space]

        [SerializeField] private Vector2 _defaultCameraSize;

        private bool _isDragging;
        private float _initialDistance;
        private float _initialOrthoSize;
        private float _previousPinchDistance;

        private Camera _mainCamera;

        private PauseProvider _pauseProvider;
        private bool _isPaused;

        public float CameraSize => Screen.width < Screen.height ?
                _defaultCameraSize.x : _defaultCameraSize.y;

        [Inject]
        private void Construct(PauseProvider pauseProvider)
        {
            _pauseProvider = pauseProvider;
            _pauseProvider.Add(this);
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _mainCamera.orthographicSize = CameraSize;
        }

        public void Pause() => _isPaused = true;
        public void Unpause() => _isPaused = false;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isPaused) return;
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isPaused) return;
            _isDragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
            {
                Vector3 delta = eventData.delta * -_dragStep;
                var position = ClampMapPosition(_camera.position + delta);

                _camera.position = position;
            }
        }

        private void Update()
        {
            if (_isPaused) return;

            HandleMouseWheelZoom();
            HandlePinchZoom();
        }

        private void HandleMouseWheelZoom()
        {
            if (_mainCamera == null) return;

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                Zoom(-scroll * _zoomSpeed);
            }
        }

        private void HandlePinchZoom()
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                {
                    _initialDistance = currentDistance;
                    _initialOrthoSize = _mainCamera.orthographicSize;
                    _previousPinchDistance = currentDistance;
                }
                else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    float deltaDistance = currentDistance - _previousPinchDistance;

                    if (Mathf.Sign(deltaDistance) != Mathf.Sign(currentDistance - _initialDistance))
                    {
                        _initialDistance = _previousPinchDistance;
                        _initialOrthoSize = _mainCamera.orthographicSize;
                    }
                    _previousPinchDistance = currentDistance;

                    float pinchAmount = (_initialDistance - currentDistance) * _pinchZoomSpeed * _initialOrthoSize;

                    Zoom(pinchAmount);
                }
            }
        }

        private void Zoom(float increment)
        {
            var bounds = Screen.width > Screen.height ? _zoomBoundsForHor : _zoomBoundsForVert;

            var zoom = _mainCamera.orthographicSize + increment;
            zoom = Mathf.Clamp(zoom, bounds.x, bounds.y);

            _mainCamera.orthographicSize = zoom;
            _camera.position = ClampMapPosition(_camera.position);
        }

        private Vector3 ClampMapPosition(Vector3 position)
        {
            var vertExtent = _mainCamera.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            position.x = Mathf.Clamp(position.x, _positionBounds.x + horzExtent, _positionBounds.z - horzExtent);
            position.y = Mathf.Clamp(position.y, _positionBounds.w + vertExtent, _positionBounds.y - vertExtent);

            if (horzExtent * 2 > Mathf.Abs(_positionBounds.z - _positionBounds.x))
                position.x = 0f;
            if (vertExtent * 2 > Mathf.Abs(_positionBounds.w - _positionBounds.y))
                position.y = 0f;

            return position;
        }

        private void OnDestroy()
        {
            _pauseProvider.Remove(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            var bottomLeft = new Vector2(_positionBounds.x, _positionBounds.y);
            var topLeft = new Vector2(_positionBounds.x, _positionBounds.w);
            var topRight = new Vector2(_positionBounds.z, _positionBounds.w);
            var bottomRight = new Vector2(_positionBounds.z, _positionBounds.y);

            Gizmos.DrawLine(bottomLeft, topLeft);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
        }
    }
}