using System;
using UnityEngine;

namespace Theme
{
    [Serializable]
    public class GradientTheme : ThemeBase
    {
        [field: SerializeField] public Gradient Gradent { get; private set; }
    }
}