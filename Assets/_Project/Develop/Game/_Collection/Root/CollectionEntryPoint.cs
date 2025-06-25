using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Collection
{
    public class CollectionEntryPoint : SceneEntryPoint
    {
        private CollectionUI _ui;

        [Inject]
        private void Construct(CollectionUI ui)
        {
            _ui = ui;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<CollectionEnterParams>());
        }

        private IEnumerator Run(CollectionEnterParams enterParams)
        {
            var isLoaded = false;

            //UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}