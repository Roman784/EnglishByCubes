using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CollectionItemConfigs", menuName = "Game Configs/Collection/New Collection Item Configs")]
    public class CollectionItemConfigs : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Sprite FilledSprite { get; private set; }
        [field: SerializeField] public Sprite Shadow { get; private set; }
        [field: SerializeField] public float FillRate { get; private set; }

        public void SetId(int id)
        {
            Id = id;
            EditorUtility.SetDirty(this);
        }
    }
}