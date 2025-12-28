using System;
using UnityEngine;

namespace Template
{
    [Serializable]
    public class TemplateSlotData
    {
        [field: SerializeField] public TemplateSlotMode Mode { get; private set; }
        [field: SerializeField] public int CubeNumber { get; private set; }
        [field: SerializeField] public int CubeSideIndex { get; private set; }
    }
}