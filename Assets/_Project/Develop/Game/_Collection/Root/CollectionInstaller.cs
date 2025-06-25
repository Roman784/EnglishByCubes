using UI;
using UnityEngine;
using Zenject;

namespace Collection
{
    public class CollectionInstaller : MonoInstaller
    {
        [SerializeField] private CollectionUI _collectionUIPrefab;

        public override void InstallBindings()
        {
            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<CollectionUI>().FromComponentInNewPrefab(_collectionUIPrefab).AsSingle();
        }
    }
}