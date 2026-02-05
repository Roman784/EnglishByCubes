using Collection;
using Pause;
using UnityEngine;
using Zenject;

namespace UI
{
    public class CollectionUI : SceneUI
    {
        [SerializeField] private ItemContentMenu _itemContentMenu;

        private CollectionEnterParams _enterParams;

        public void Init(CollectionEnterParams enterParams)
        {
            _enterParams = enterParams;
        }

        public void OpenItemContent(CollectionItemName name)
        {
            var item = GameConfigs.CollectionConfigs.GetItem(name);
            _itemContentMenu.Open(item);
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }
    }
}