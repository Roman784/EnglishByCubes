using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class BlockFactory : PlaceholderFactory<BlockView, Block>
    {
        private readonly DiContainer _container;
        private readonly BlockView _prefab;

        [Inject]
        public BlockFactory(DiContainer container, BlockView prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public Block Create()
        {
            var view = Object.Instantiate(_prefab);
            var newBlock = _container.Instantiate<Block>(new object[] { view });

            return newBlock;
        }
    }
}