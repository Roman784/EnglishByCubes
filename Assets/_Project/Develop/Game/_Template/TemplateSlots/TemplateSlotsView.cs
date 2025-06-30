using UnityEngine;
using UnityEngine.UI;

namespace Template
{
    public class TemplateSlotsView : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _container;

        [Space]

        [SerializeField] private float _maxCellSize;

        private void Awake()
        {
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
            var totalScreenWidth = Screen.width;
            var spacing = _container.spacing.x;

            var size = (totalScreenWidth - (slotsCount + 1) * spacing) / slotsCount;
            size = Mathf.Clamp(size, 0, _maxCellSize);

            _container.cellSize = Vector2.one * size;
        }
    }
}