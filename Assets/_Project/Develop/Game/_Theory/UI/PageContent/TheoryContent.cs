using System;
using UnityEngine;

namespace Theory
{
    [Serializable]
    public class TheoryContent
    {
        [field: SerializeField] public TheoryContentTag Tag { get; private set; }

        public object Content
        {
            get
            {
                switch (Tag)
                {
                    case TheoryContentTag.Text: return _text;
                    case TheoryContentTag.Table: return _table;
                    case TheoryContentTag.CubeUnfolding: return _cubeUnfolding;
                    default: return null;
                }
            }
        }

        [SerializeField, TextArea(3, 10)] private string _text;
        [SerializeField] private TheoryTable _table;
        [SerializeField] private TheoryCubeUnfolding _cubeUnfolding;
    }
}