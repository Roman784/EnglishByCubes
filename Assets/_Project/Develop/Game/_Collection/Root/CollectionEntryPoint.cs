using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Collection
{
    public class CollectionEntryPoint : SceneEntryPoint
    {
        [SerializeField] private MapGenerator _mapGenerator;

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

            var collectedItemIds = _gameStateProvider.GameStateProxy.State.CollectedCollectionItems;

            // Map generation.
            _mapGenerator.Generate(collectedItemIds);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            yield return null;

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}