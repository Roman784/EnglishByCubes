using System;
using UnityEngine;

namespace Template
{
    [Serializable]
    public class TemplateSlotData
    {
        [field: SerializeField] public TemplateSlotMode Mode { get; private set; }
    }
}