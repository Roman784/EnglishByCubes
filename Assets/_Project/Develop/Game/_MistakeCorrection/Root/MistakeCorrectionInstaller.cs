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
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeView, Cube, CubeFactory>().AsTransient();
        }

        private void BindServices()
        {
            Container.Bind<CubesPositionPreviewService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<MistakeCorrectionUI>().FromComponentInNewPrefab(_uiPrefab).AsSingle();
        }
    }
}