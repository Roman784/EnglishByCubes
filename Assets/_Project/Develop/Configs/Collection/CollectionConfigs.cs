using Collection;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CollectionConfigs", menuName = "Game Configs/Collection/New Collection Configs")]
    public class CollectionConfigs : ScriptableObject
    {
        [Serializable]
        public class CollectionItem
        {
            [field: SerializeField] public int Id { get; private set; }
            [field: SerializeField] public float FillRate { get; private set; }
            [field: SerializeField] public CollectionItemConfigs Configs { get; private set; }

            public void SetId(int id) => Id = id;
        }

        [field: SerializeField] public List<CollectionItem> Items { get; private set; }

        public CollectionItem GetUncollectedItem(List<int> collectedItemIds)
        {
            foreach (var item in Items)
            {
                if (!collectedItemIds.Contains(item.Id))
                    return item;
            }
            return null;
        }

        public CollectionItemConfigs GetItem(int id)
        {
            foreach (var item in Items)
            {
                if (item.Id == id)
                    return item.Configs;
            }

            Debug.LogError($"Item with id {id} not found!");
            return null;
        }

        public CollectionItemConfigs GetItem(CollectionItemName name)
        {
            foreach (var item in Items)
            {
                if (item.Configs.Name == name)
                    return item.Configs;
            }

            Debug.LogError($"Item with name {name} not found!");
            return null;
        }

        [ContextMenu("Set ids")]
        private void SetIds()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].SetId(i);
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
        }

        private void OnValidate()
        {
            ValidateIds();
        }

        private void ValidateIds()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                for (int j = i+1; j < Items.Count; j++)
                {
                    if (Items[i].Id == Items[j].Id)
                        Debug.LogError($"Collection item ids {Items[i].Id} are repeated!");
                }
            }
        }
    }
}