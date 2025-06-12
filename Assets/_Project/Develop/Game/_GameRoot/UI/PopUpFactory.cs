using Zenject;

namespace UI
{
    public abstract class PopUpFactory<T> : PlaceholderFactory<T> where T : PopUp
    {
        private UIRoot _uiRoot;

        [Inject]
        private void Construct(UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
        }

        public new T Create()
        {
            var root = _uiRoot.PopUpsRoot;

            T popUp = base.Create();
            root.AttachPopUp(popUp);

            return popUp;
        }
    }
}
