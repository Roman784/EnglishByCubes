using UI;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    public abstract class LevelConfigs : ScriptableObject
    {
        [field: SerializeField] public int GlobalNumber { get; private set; }
        [field: SerializeField] public int LocalNumber { get; private set; }
        public abstract LevelMode Mode { get; }

        public void SetGlobalNumber(int number)
        {
            GlobalNumber = number;
            EditorUtility.SetDirty(this);
        }

        public void SetLocalNumber(int number)
        {
            LocalNumber = number;
            EditorUtility.SetDirty(this);
        }

        public T As<T>() where T : LevelConfigs
        {
            return (T)this;
        }
    }
}