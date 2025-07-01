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
        [SerializeField] private TemplateSlotsView _templateSlotsViewPrefab;
        [SerializeField] private TemplateSlot _templateSlotPrefab;

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
            Container.Bind<TemplateSlotsView>().FromInstance(_templateSlotsViewPrefab).AsTransient();
            Container.Bind<TemplateSlot>().FromInstance(_templateSlotPrefab).AsTransient();
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeView, Cube, CubeFactory>().AsTransient();
            Container.BindFactory<SlotBarView, SlotBar, SlotBarFactory>().AsTransient();
            Container.BindFactory<TemplateSlotsView, TemplateSlots, TemplateSlotsFactory>().AsTransient();
        }

        private void BindServices()
        {
            Container.Bind<IGameFieldService>().To<TemplateFieldService>().AsSingle();
            Container.Bind<ICubesLayoutService>().To<TemplateCubesLayoutService>().AsSingle();
            Container.Bind<ILevelPassingService>().To<TemplateLevelPassingService>().AsSingle();
            Container.Bind<CubesPositionPreviewService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<TemplateUI>().FromComponentInNewPrefab(_templateUIPrefab).AsSingle();
        }
    }
}