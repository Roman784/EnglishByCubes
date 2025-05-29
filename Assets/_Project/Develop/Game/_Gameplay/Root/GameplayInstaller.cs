using UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CubeView _cubePrefab;
        [SerializeField] private GameplayUI _gameplayUIPrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindCubes();
            BindUI();
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeView, Cube, CubeFactory>().AsTransient();
        }

        private void BindCubes()
        {
            Container.Bind<CubeView>().FromInstance(_cubePrefab).AsTransient();
        }

        private void BindUI()
        {
            Container.Bind<GameplayUI>().FromComponentInNewPrefab(_gameplayUIPrefab).AsSingle();
        }
    }
}