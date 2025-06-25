using Collection;

namespace UI
{
    public class CollectionUI : SceneUI
    {
        private CollectionEnterParams _enterParams;

        public void Init(CollectionEnterParams enterParams)
        {
            _enterParams = enterParams;
        }

        public void OpenPreviousScene()
        {
            _sceneProvider.OpenPreviousScene(_enterParams);
        }
    }
}