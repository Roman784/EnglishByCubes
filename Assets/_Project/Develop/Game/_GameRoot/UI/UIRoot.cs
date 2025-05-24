using R3;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class UIRoot
    {
        private UIRootView _view;

        public UIRoot(UIRootView view)
        {
            _view = view;
        }

        public IEnumerator SetLoadingScreen(bool value)
        {
            bool isCompleted = false;
            _view.SetLoadingScreen(value).Subscribe(_ => isCompleted = true);

            yield return new WaitUntil(() => isCompleted);
        }
    }
}