using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProceduralAnimations
{
    public class ShadowShifting : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Shadow _target;
        [SerializeField] private Vector2 _endShadowScale = new Vector2(0, -6f);
        [SerializeField] private float _duration = 0.05f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        private RectTransform _rectTarget;
        private Vector2 _initialShadowScale;
        private Vector2 _initalPosition;
        private Tween _shadowChangingTween;
        private Tween _positionChangingTween;

        private void Awake()
        {
            _rectTarget = _target.GetComponent<RectTransform>();
            _initialShadowScale = _target.effectDistance;
            _initalPosition = _rectTarget.anchoredPosition;
        }

        private void Update()
        {
#if (UNITY_IOS || UNITY_ANDROID || UNITY_WEBGL) && !UNITY_EDITOR
            if (UnityEngine.Device.Application.isMobilePlatform && 
                Input.touchCount == 0 && _target.effectDistance != _initialShadowScale)
            {
                ShiftUp();
            }
#endif
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ShiftDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ShiftUp();
        }

        private void ShiftDown()
        {
            var newPosition = _rectTarget.anchoredPosition + (_initialShadowScale - _endShadowScale);

            _shadowChangingTween?.Kill();
            _positionChangingTween?.Kill();

            _shadowChangingTween = 
                DOTween.To(
                    () => _target.effectDistance,
                    x => _target.effectDistance = x,
                    _endShadowScale,
                    _duration)
                .SetEase(_ease);

            _positionChangingTween = 
                _rectTarget.DOAnchorPos(newPosition, _duration)
                .SetEase(_ease);
        }

        private void ShiftUp()
        {
            _shadowChangingTween?.Kill();
            _positionChangingTween?.Kill();

            _shadowChangingTween = 
                DOTween.To(
                    () => _target.effectDistance,
                    x => _target.effectDistance = x,
                    _initialShadowScale,
                    _duration
                ).SetEase(_ease);

            _positionChangingTween = 
                _rectTarget.DOAnchorPos(_initalPosition, _duration)
                .SetEase(_ease);
        }
    }
}