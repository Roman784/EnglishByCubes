using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CubeView _cubePrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindCubes();
        }

        private void BindFactories()
        {
            Container.BindFactory<CubeView, Cube, CubeFactory>().AsTransient();
        }

        private void BindCubes()
        {
            Container.Bind<CubeView>().FromInstance(_cubePrefab).AsTransient();
        }
    }
}