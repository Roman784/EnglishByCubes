using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CollectionConfigs", menuName = "Game Configs/Collection/New Collection Configs")]
    public class CollectionConfigs : ScriptableObject
    {
        [field: SerializeField] public List<CollectionItemConfigs> Items { get; private set; }

        public CollectionItemConfigs GetUncollectedItem(List<int> collectedItemIds)
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
                    return item;
            }

            Debug.LogError($"Item with id {id} not found!");
            return null;
        }

        [ContextMenu("Set ids")]
        private void SetIds()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].SetId(i);
            }

            //EditorUtility.SetDirty(this);
            //AssetDatabase.Refresh();
            //AssetDatabase.SaveAssets();
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