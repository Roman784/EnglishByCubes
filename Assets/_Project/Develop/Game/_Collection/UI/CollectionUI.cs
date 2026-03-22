using Collection;
using Pause;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CollectionUI : SceneUI
    {
        [SerializeField] private ItemContentMenu _itemContentMenu;
        [SerializeField] private Transform _hint;

        private CollectionEnterParams _enterParams;

        public void Init(CollectionEnterParams enterParams)
        {
            _enterParams = enterParams;
        }

        public void OpenItemContent(CollectionItemName name)
        {
            var item = GameConfigs.CollectionConfigs.GetItem(name);
            _itemContentMenu.Open(item);

            HideHint();
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }

        public void HideHint()
        {
            _hint.gameObject.SetActive(false);
        }

        private void Update()
        {
            _hint.localScale = Vector2.one * Mathf.Abs(Mathf.Sin(Time.time)) * 0.2f + Vector2.one;
        }
    }
}