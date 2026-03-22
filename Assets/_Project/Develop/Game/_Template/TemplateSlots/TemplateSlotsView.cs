using UnityEngine;
using UnityEngine.UI;

namespace Template
{
    public class TemplateSlotsView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GridLayoutGroup _container;

        [Space]

        [SerializeField] private float _maxCellSize;

        private float _baseAspect = 1.777f;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;

            var cameraAngles = Camera.main.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(cameraAngles.x - 90, 0f, 0f);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void AddSlot(TemplateSlot slot)
        {
            slot.transform.SetParent(_container.transform, false);
        }

        public void SetContainerCellSize(int slotsCount)
        {
            var aspect = (float)Screen.height / (float)Screen.width;
            var totalScreenWidth = Screen.width;
            var spacing = _container.spacing.x;
            var max = (_maxCellSize / _baseAspect) * aspect;

            var size = (totalScreenWidth - (slotsCount - 1) * spacing - 64) / slotsCount;

            size = Mathf.Clamp(size, 0, max);

            _container.cellSize = Vector2.one * size;
        }
    }
}