using System;
using UnityEngine;

namespace Theme
{
    [Serializable]
    public class ColorTheme : ThemeBase
    {
        [field: SerializeField] public Color Color { get; private set; }
    }
}