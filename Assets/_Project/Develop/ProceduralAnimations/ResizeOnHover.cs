using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProceduralAnimations
{
    public class ResizeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform _target;
        [SerializeField] private Vector2 _initialSize;
        [SerializeField] private Vector2 _endSize = new Vector2(100, 100);
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private Ease _ease = Ease.OutBack;

        private Tween _currentTween;

        private void Awake()
        {
            _target.sizeDelta = _initialSize;
        }

        private void OnDisable()
        {
            _target.sizeDelta = _initialSize;
            _currentTween?.Kill();
        }

        private void Update()
        {
#if (UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            if (UnityEngine.Device.Application.isMobilePlatform && 
                Input.touchCount == 0 && _target.sizeDelta != _initialSize)
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
            _currentTween = _target.DOSizeDelta(_endSize, _duration).SetEase(_ease);
        }

        public void ZoomOut()
        {
            _currentTween?.Kill();
            _currentTween = _target.DOSizeDelta(_initialSize, _duration).SetEase(_ease);
        }

        [ContextMenu("Fast Setup")]
        private void FastSetup()
        {
            if (TryGetComponent(out RectTransform component))
            {
                _target = component;
                _initialSize = Vector2.zero;
                _endSize = _target.sizeDelta;
            }
        }
    }
}