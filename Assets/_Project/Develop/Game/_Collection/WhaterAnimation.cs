using UnityEngine;
using UnityEngine.UI;

namespace Collection
{
    [RequireComponent(typeof(RawImage))]
    public class WhaterAnimation : MonoBehaviour
    {
        private RawImage _image;

        [SerializeField, Range(0, 10)] private float _scrollSpeed = 0.1f;

        [SerializeField, Range(-1, 1)] private float _xDirection = 1;
        [SerializeField, Range(-1, 1)] private float _yDirection = 1;

        [SerializeField] private float _sizeChangingFrequency = 1f;
        [SerializeField] private float _sizeChangingAmplitude = 0.1f;

        private void Awake()
        {
            _image = GetComponent<RawImage>();
        }

        private void Update()
        {
            var position = new Vector2(-_xDirection * _scrollSpeed, _yDirection * _scrollSpeed) * Time.time / 10;
            var size = new Vector2(_image.uvRect.size.x, _image.uvRect.size.y + Mathf.Sin(Time.time * _sizeChangingFrequency) * _sizeChangingAmplitude);

            _image.uvRect = new Rect(position, size);
        }
    }
}