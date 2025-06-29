using Gameplay;
using GameRoot;
using UI;
using UnityEngine;
using Zenject;

namespace Template
{
    public class TemplateInstaller : MonoInstaller
    {
        [SerializeField] private TemplateUI _templateUIPrefab;
        [SerializeField] private CubeView _cubePrefab;
        [SerializeField] private SlotBarView _slotBarPrefab;

        public override void InstallBindings()
        {
            BindPrefabs();
            BindFactories();
            BindServices();
            BindUI();
        }

        private void BindPrefabs()
        {
            Container.Bind<CubeView>().FromInstance(_cubePrefab).AsTransient();
            Container.Bind<SlotBarView>().FromInstance(_slotBarPrefab).AsTransient();
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeView, Cube, CubeFactory>().AsTransient();
            Container.BindFactory<SlotBarView, SlotBar, SlotBarFactory>().AsTransient();
        }

        private void BindServices()
        {
            Container.Bind<IGameFieldService>().To<TemplateFieldService>().AsSingle();
            Container.Bind<CubesLayoutService>().AsSingle();
            Container.Bind<CubesPositionPreviewService>().AsSingle();
            Container.Bind<TaskPassingService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<TemplateUI>().FromComponentInNewPrefab(_templateUIPrefab).AsSingle();
        }
    }
}