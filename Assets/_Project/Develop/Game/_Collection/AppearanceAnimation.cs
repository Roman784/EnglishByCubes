using DG.Tweening;
using UnityEngine;

namespace Collection
{
    public class AppearanceAnimation : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private MapController _mapController;

        private void Start()
        {
            _camera.transform.position += Vector3.up * 6;
            _camera.orthographicSize = _mapController.CameraSize + 8f;
        }

        public void PlayAnimation()
        {
            _camera.transform.DOMoveY(0, 1.5f).SetEase(Ease.InOutQuad);
            _camera.DOOrthoSize(_mapController.CameraSize, 2f).SetEase(Ease.InOutQuad);
        }
    }
}