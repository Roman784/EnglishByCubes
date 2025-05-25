using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private BlockView _blockPrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindBlocks();
        }

        private void BindFactories()
        {
            Container.BindFactory<BlockView, Block, BlockFactory>().AsTransient();
        }

        private void BindBlocks()
        {
            Container.Bind<BlockView>().FromInstance(_blockPrefab).AsTransient();
        }
    }
}