using System;
using UnityEngine;

namespace Theme
{
    [Serializable]
    public class TextTheme : ThemeBase
    {
        [field: SerializeField] public Color Dark { get; private set; }
        [field: SerializeField] public Color Light { get; private set; }
    }
}