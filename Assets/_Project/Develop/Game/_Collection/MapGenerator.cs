using Audio;
using Configs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Zenject;

namespace Collection
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private List<Tree> _trees;
        [SerializeField] private List<CollectionItem> _items;

        [SerializeField] private float _initialDelay;
        [SerializeField] private float _treesAppearDelay;
        [SerializeField] private float _itemsAppearDelay;

        private IConfigsProvider _configsProvider;

        [Inject]
        private void Construct(AudioProvider audioProvider, IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void Generate(IEnumerable<int> unclokedItemsIds)
        {
            Coroutines.Start(GenerateRoutine(unclokedItemsIds));
        }

        private IEnumerator GenerateRoutine(IEnumerable<int> unclokedItemsIds)
        {
            yield return new WaitForSeconds(_initialDelay);

            Coroutines.Start(AppearTrees());

            yield return new WaitForSeconds(_trees.Count * _treesAppearDelay / 2f);

            yield return AppearItems(unclokedItemsIds);
        }

        private IEnumerator AppearTrees()
        {
            foreach (var tree in _trees)
            {
                tree.Appear();
                yield return new WaitForSeconds(_treesAppearDelay);
            }
        }

        private IEnumerator AppearItems(IEnumerable<int> unclokedItemsIds)
        {
            foreach (var item in _items)
            {
                var id = GetItemId(item.Name);
                item.Appear(unclokedItemsIds.Contains(id));
                yield return new WaitForSeconds(_itemsAppearDelay);
            }
        }

        private int GetItemId(CollectionItemName name)
        {
            foreach (var item in _configsProvider.GameConfigs.CollectionConfigs.Items)
            {
                if (item.Configs.Name == name)
                    return item.Id;
            }
            return -1;
        }
    }
}