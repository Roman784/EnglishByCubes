using Collection;
using UnityEngine;

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

        public void OpenItemContent(int id)
        {
            var item = GameConfigs.CollectionConfigs.GetItem(id);
            _itemContentMenu.Open(item);
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }
    }
}