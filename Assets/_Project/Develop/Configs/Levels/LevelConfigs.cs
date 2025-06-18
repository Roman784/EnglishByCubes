using UI;
using UnityEngine;

namespace Configs
{
    public abstract class LevelConfigs : ScriptableObject
    {
        [field: SerializeField] public int Number { get; private set; }
        public abstract LevelMode Mode { get; }

        public void SetNumber(int number)
        {
            Number = number;
        }

        public T As<T>() where T : LevelConfigs
        {
            return (T)this;
        }
    }
}