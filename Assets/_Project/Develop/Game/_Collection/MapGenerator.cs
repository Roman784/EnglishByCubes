using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Collection
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private List<Tree> _trees;
        [SerializeField] private List<CollectionItem> _items;

        [SerializeField] private float _initialDelay;
        [SerializeField] private float _treesAppearDelay;
        [SerializeField] private float _itemsAppearDelay;

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
                item.Appear(unclokedItemsIds.Contains(item.Id));
                yield return new WaitForSeconds(_itemsAppearDelay);
            }
        }
    }
}