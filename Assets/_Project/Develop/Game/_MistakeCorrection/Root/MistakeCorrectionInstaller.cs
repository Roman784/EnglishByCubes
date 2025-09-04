using Gameplay;
using GameRoot;
using UI;
using UnityEngine;
using Zenject;

namespace MistakeCorrection
{
    public class MistakeCorrectionInstaller : MonoInstaller
    {
        [SerializeField] private MistakeCorrectionUI _uiPrefab;
        [SerializeField] private CubeView _cubePrefab;

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
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeView, Cube, CubeFactory>().AsTransient();
        }

        private void BindServices()
        {
            Container.Bind<CubesPositionPreviewService>().AsSingle();
            Container.Bind<IGameFieldService>().To<MistakeCorrectionFieldService>().AsSingle();
            Container.Bind<ICubesLayoutService>().To<GameplayCubesLayoutService>().AsSingle();
            Container.Bind<ILevelPassingService>().To<MistakeCorrectionLevelPassingService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<MistakeCorrectionUI>().FromComponentInNewPrefab(_uiPrefab).AsSingle();
        }
    }
}