using System;
using UnityEngine;

namespace Theme
{
    [Serializable]
    public class ThemeBase
    {
        [field: SerializeField] public ThemeTags Tag { get; private set; }
    }
}