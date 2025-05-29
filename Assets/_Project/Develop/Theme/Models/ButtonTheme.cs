using System;
using UnityEngine;

namespace Theme
{
    [Serializable]
    public class ButtonTheme : ThemeBase
    {
        [field: SerializeField] public Color Icon { get; private set; }
        [field: SerializeField] public Color Top { get; private set; }
        [field: SerializeField] public Color Side { get; private set; }
    }
}