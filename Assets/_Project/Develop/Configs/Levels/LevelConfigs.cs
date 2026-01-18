using UI;
using UnityEditor;
using UnityEngine;

namespace Configs
{
    public abstract class LevelConfigs : ScriptableObject
    {
        [field: SerializeField] public int GlobalNumber { get; private set; }
        [field: SerializeField] public int LocalNumber { get; private set; }
        [field: SerializeField] public string SectionTitle { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public string SentanceSign { get; private set; } = "";
        public abstract LevelMode Mode { get; }

        public void SetGlobalNumber(int number)
        {
            GlobalNumber = number;
        }

        public void SetLocalNumber(int number)
        {
            LocalNumber = number;
        }

        public T As<T>() where T : LevelConfigs
        {
            return (T)this;
        }

        public virtual void Validate() { }
    }
}