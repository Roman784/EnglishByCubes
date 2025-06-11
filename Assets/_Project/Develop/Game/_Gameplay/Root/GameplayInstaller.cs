using UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayUI _gameplayUIPrefab;
        [SerializeField] private CubeView _cubePrefab;
        [SerializeField] private SlotBarView _slotBarPrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindPrefabs();
            BindServices();
            BindUI();
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeView, Cube, CubeFactory>().AsTransient();
            Container.BindFactory<SlotBarView, SlotBar, SlotBarFactory>().AsTransient();
        }

        private void BindPrefabs()
        {
            Container.Bind<CubeView>().FromInstance(_cubePrefab).AsTransient();
            Container.Bind<SlotBarView>().FromInstance(_slotBarPrefab).AsTransient();
        }

        private void BindServices()
        {
            Container.Bind<GameFieldService>().AsSingle();
            Container.Bind<CubesLayoutService>().AsSingle();
            Container.Bind<CubesPositionPreviewService>().AsSingle();
            Container.Bind<TaskPassingService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<GameplayUI>().FromComponentInNewPrefab(_gameplayUIPrefab).AsSingle();
        }
    }
}