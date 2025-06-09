using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine;

namespace ProceduralAnimations
{
    public class RescaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _initialScale;
        [SerializeField] private Vector3 _endScale = Vector3.one;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        private Tween _currentTween;

        private void Awake()
        {
            _target.localScale = _initialScale;
        }

        private void OnDisable()
        {
            _target.localScale = _initialScale;
            _currentTween?.Kill();
        }

        private void Update()
        {
#if (UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            if (UnityEngine.Device.Application.isMobilePlatform && 
                Input.touchCount == 0 && _target.sizeDelta != _initialScale)
            {
                ZoomOut();
            }
#endif
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ZoomIn();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ZoomOut();
        }

        public void ZoomIn()
        {
            _currentTween?.Kill();
            _currentTween = _target.DOScale(_endScale, _duration).SetEase(_ease);
        }

        public void ZoomOut()
        {
            _currentTween?.Kill();
            _currentTween = _target.DOScale(_initialScale, _duration).SetEase(_ease);
        }

        [ContextMenu("Fast Setup")]
        private void FastSetup()
        {
            if (TryGetComponent(out Transform component))
            {
                _target = component;
                _initialScale = Vector2.zero;
                _endScale = _target.localScale;
            }
        }
    }
}