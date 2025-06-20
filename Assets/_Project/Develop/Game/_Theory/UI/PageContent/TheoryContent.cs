using System;
using UnityEngine;

namespace Theory
{
    [Serializable]
    public class TheoryContent
    {
        [field: SerializeField] public TheoryContentTag Tag { get; private set; }

        public GameObject Content
        {
            get
            {
                switch (Tag)
                {
                    case TheoryContentTag.Text: return _text.gameObject;
                    case TheoryContentTag.Table: return _table.gameObject;
                    case TheoryContentTag.CubeUnfolding: return _cubeUnfolding.gameObject;
                    default: return null;
                }
            }
        }

        [SerializeField] private TheoryText _text;
        [SerializeField] private TheoryTable _table;
        [SerializeField] private TheoryCubeUnfolding _cubeUnfolding;
    }
}