using Collection;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CollectionItemConfigs", menuName = "Game Configs/Collection/New Collection Item Configs")]
    public class CollectionItemConfigs : ScriptableObject
    {
        [field: SerializeField] public CollectionItemName Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Sprite FilledSprite { get; private set; }

        [field: Header("Content in collection")]
        [field: SerializeField] public Sprite Picture { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, TextArea(3, 10)] public string MainText { get; private set; }
    }
}